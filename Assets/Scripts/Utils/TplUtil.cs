using Survivor.Template;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Survivor.Utils
{
    class TplUtil
    {
        // 单例字典，缓存加载的模板数据
        private static Dictionary<Type, object> singletons = new Dictionary<Type, object>();

        // 获取模板映射字典
        public static Dictionary<int, V> GetTplMap<T, V>() where T : BaseTpl<V>, new() where V : BaseTplInfo, new()
        {
            return GetTpl<T, V>().Dic;
        }

        // 获取模板列表
        public static List<V> GetTplList<T, V>() where T : BaseTpl<V>, new() where V : BaseTplInfo, new()
        {
            return GetTpl<T, V>().List;
        }

        public static T GetTpl<T, V>() where T : BaseTpl<V>, new() where V : BaseTplInfo, new()
        {
            Type type = typeof(T);

            // 检查是否已经加载过
            if (!singletons.ContainsKey(type))
            {
                T tpl = new T();
                // 加载 XML 模板
                tpl = XmlUtil.LoadXMLTpl<T, V>(tpl.TplPath);
                singletons[type] = tpl;
            }
            return singletons[type] as T;
        }

        public static List<HeroTplInfo> GetHeroList()
        {
            return GetTplList<HeroTpl, HeroTplInfo>();
        }

        public static Dictionary<int, HeroTplInfo> GetHeroMap()
        {
            return GetTplMap<HeroTpl, HeroTplInfo>();
        }

        public static List<EnemyTplInfo> GetEnemyList()
        {
            return GetTplList<EnemyTpl, EnemyTplInfo>();
        }

        public static Dictionary<int, EnemyTplInfo> GetEnemyMap()
        {
            return GetTplMap<EnemyTpl, EnemyTplInfo>();
        }

        public static List<WeaponTplInfo> GetWeaponList()
        {
            return GetTplList<WeaponTpl, WeaponTplInfo>();
        }

        public static Dictionary<int,WeaponTplInfo> GetWeaponMap()
        {
            return GetTplMap<WeaponTpl, WeaponTplInfo>();
        }

        public static List<ItemTplInfo> GetItemList()
        {
            return GetTplList<ItemTpl, ItemTplInfo>();
        }

        public static Dictionary<int, ItemTplInfo> GetItemMap()
        {
            return GetTplMap<ItemTpl, ItemTplInfo>();
        }

        public static List<WaveTplInfo> GetWaveList()
        {
            return GetTplList<WaveTpl, WaveTplInfo>();
        }

        public static Dictionary<int, WaveTplInfo> GetWaveMap()
        {
            return GetTplMap<WaveTpl, WaveTplInfo>();
        }

        public static Dictionary<int, List<WaveTplInfo>> GetWaveDictionaryWithWaveKey()
        {
            Dictionary<int, List<WaveTplInfo>> res = new Dictionary<int, List<WaveTplInfo>>();
            var waveList = GetWaveList();
            foreach(var info in waveList)
            {
                if (!res.ContainsKey(info.Wave))
                {
                    res[info.Wave] = new List<WaveTplInfo>();
                }
                res[info.Wave].Add(info);
            }
            return res;
        }


        public static List<DropTplInfo> GetDropList()
        {
            return GetTplList<DropTpl, DropTplInfo>();
        }

        public static Dictionary<int, DropTplInfo> GetDropMap()
        {
            return GetTplMap<DropTpl, DropTplInfo>();
        }

        public static List<BuffTplInfo> GetBuffList()
        {
            return GetTplList<BuffTpl, BuffTplInfo>();
        }

        public static Dictionary<int, BuffTplInfo> GetBuffMap()
        {
            return GetTplMap<BuffTpl, BuffTplInfo>();
        }

    }
}
