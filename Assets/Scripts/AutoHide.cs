using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    // 延迟时间
    public float delay = 0.5f;
    public bool destory = false;

    // 启动时调用
    void Start()
    {
        // 调用 Hide 方法，延迟 delay 秒
        if(gameObject.activeSelf) Invoke("Hide", delay);
    }

    // 每次对象被启用时调用
    void OnEnable()
    {
        // 调用 Hide 方法，延迟 delay 秒
        Invoke("Hide", delay);
    }

    // 隐藏游戏对象的方法
    void Hide()
    {
        gameObject.SetActive(false);
        if (destory) Destroy(gameObject);
    }
}
