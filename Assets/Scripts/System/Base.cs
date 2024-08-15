using System;
using UnityEngine;

namespace Survivor.Base
{

    /// <summary>
    /// 修饰T为一个静态类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StaticInstantce<T> : MonoBehaviour where T: MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 修饰T为一个单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : StaticInstantce<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else base.Awake();
        }
    }

    /// <summary>
    /// 修饰T为持久化的单例，DontDestroyOnLoad
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }


}
