using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������������еĹ������壬ֻ�����з���ҽ�ɫ��ײ
public class �ֵ������� : BaseAttackObject 
{

    public List<string> �ֵ�tag;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (�ֵ�tag.Contains(collision.tag))
        {
            Destroy(collision.gameObject);
        }

    }
}
