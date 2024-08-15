using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// 道具工厂类
public static class BuffFactory
{
    private static Dictionary<int, Type> buffTypeMap = new Dictionary<int, Type>();

    static BuffFactory()
    {
        AutoRegisterBuff();
    }

    public static void RegisterBuff(int id, Type type)
    {
        if (!buffTypeMap.ContainsKey(id))
        {
            buffTypeMap[id] = type;
        }
        else
        {
            Debug.LogError("buff id：" + id + "重复，" + buffTypeMap[id] + "和" + type);
        }
    }

    public static BaseBuff GetBuffByID(int id)
    {

        if (buffTypeMap.ContainsKey(id))
        {
            Type type = buffTypeMap[id];
            return (BaseBuff)Activator.CreateInstance(type);
        }
        else
        {
            // 没有找到对应的道具
            Debug.LogError("未找到ID为 " + id + " 的buff类型");
            return null;
        }
    }

    // 通过反射自动注册道具类型
    public static void AutoRegisterBuff()
    {
        var itemType = typeof(BaseBuff);
        var assembly = Assembly.GetAssembly(itemType);

        var itemTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(itemType));

        foreach (var type in itemTypes)
        {
            var instance = (BaseBuff)Activator.CreateInstance(type);
            RegisterBuff(instance.ID, type);
        }
    }
}

