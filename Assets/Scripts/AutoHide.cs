using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    // �ӳ�ʱ��
    public float delay = 0.5f;
    public bool destory = false;

    // ����ʱ����
    void Start()
    {
        // ���� Hide �������ӳ� delay ��
        if(gameObject.activeSelf) Invoke("Hide", delay);
    }

    // ÿ�ζ�������ʱ����
    void OnEnable()
    {
        // ���� Hide �������ӳ� delay ��
        Invoke("Hide", delay);
    }

    // ������Ϸ����ķ���
    void Hide()
    {
        gameObject.SetActive(false);
        if (destory) Destroy(gameObject);
    }
}
