using System;
using System.Collections.Generic;
using UnityEngine;

// 每个Character都有一个BuffList，用于存放所有的buff
[Serializable]
public class BuffList
{
    public LinkedList<BaseBuff> buffs = new LinkedList<BaseBuff>();

    public void AddBuff(int id, GameObject creater, GameObject owner, int stack=1)
    {
        var buff = BuffFactory.GetBuffByID(id);
        buff.creater = creater;
        buff.owner = owner;
        buff.curStack = stack;

        var node = Find(buff);

        if(node == null)
        {
            // 没有同一个buff
            buffs.AddLast(buff);
            buff.OnAdd();
        }
        else
        {
            if(node.Value.ID == buff.ID)
            {
                switch (node.Value.AddType) 
                {
                    case BuffAddType.Keep:
                        break;
                    case BuffAddType.Replace:
                        node.Value = buff;
                        break;
                    case BuffAddType.Modify:
                        node.Value.AddModify();
                        break;
                    default:
                        return;
                }
                node.Value.OnAdd();
            }
            else
            {
                // 没有同一个buff
                buffs.AddBefore(node, buff);
                buff.OnAdd();

            }
        }
        

    }

    public bool RemoveBuff(int id)
    {
        foreach (var buff in buffs)
        {
            if (buff.ID == id)
            {
                switch (buff.RemoveType)
                {
                    case BuffRemoveType.Remove:
                        buffs.Remove(buff);
                        break;
                    case BuffRemoveType.ReduceStack:
                        buff.ReduceStack();
                        if(buff.curStack == 0) buffs.Remove(buff);
                        break;
                    default:
                        return false;
                }
                buff.OnRemove();
                return true;
            }
        }
        return false;
    }

    public BaseBuff GetBuffById(int id)
    {
        foreach(var buff in buffs)
        {
            if(buff.ID == id)
            {
                return buff;
            }
        }
        return null;
    }


    /// <summary>
    /// 如果找到ID相同的buff，返回该buff
    /// 如果没有找到相同的buff，返回需要插入位置的后一个位置（null,或者值）
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    private LinkedListNode<BaseBuff> Find(BaseBuff buff)
    {
        LinkedListNode<BaseBuff> current = buffs.First;

        while (current != null)
        {
            if (current.Value.ID == buff.ID || current.Value.Info.Priority > buff.Info.Priority)
            {
                return current;
            }
            current = current.Next;
        }

        return current;
    }


}

