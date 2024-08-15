using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������������еĹ������壬ֻ����ҽ�ɫ��ײ
public class ����AttackObject : BaseAttackObject 
{
    private Vector2 dir;

    public List<string> collideTag;

    public float ��������;

    public bool ˮƽ��ֱ�ƶ� = false;

    public bool useParentPos = true;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collideTag.Contains(collision.tag))
        {
            //Debug.Log("����");
            dir = (collision.transform.position - (useParentPos ? transform.parent.position : transform.position)).normalized;
            if (ˮƽ��ֱ�ƶ�)
            {
                // ��dir�ͽ��ƶ���ˮƽ��ֱ�ķ�����
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    dir = new Vector2(Mathf.Sign(dir.x), 0);
                }
                else
                {
                    dir = new Vector2(0, Mathf.Sign(dir.y));
                }
            }

            collision.GetComponentInParent<Rigidbody2D>().AddForce(dir * Time.deltaTime * �������� * 20000);
        }

    }
}
