using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ��е��� : Enemy
{
    public float dive��ȴʱ�� = 5f;
    private float diveClk;

    public bool isDive = false;
    private int awayFlag = 0; // 0��ʾ��֪����1��ʾ������-1��ʾԶ��

    public float disThreshold = 1f;


    protected override void Start()
    {
        base.Start();
        diveClk = dive��ȴʱ��;
    }

    protected override void Update()
    {
        base.Update();
        if (!isDive)
        {
            diveClk -= Time.deltaTime;
        }

    }

    public override void ��ս����()
    {
        // ��ת����
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        // �ٹ���
        animator.SetTrigger("attack");
    }

    public override void �ƶ�()
    {
        
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }
        // ����һ�º�Player�ľ���
        float dis = Vector2.Distance((Vector2)targetPos, (Vector2)transform.position);

        Vector2 moveDir = Vector2.zero;
        if (awayFlag == 0)
        {
            awayFlag = dis >= disThreshold ? 1 : -1;
        }

        if(awayFlag == 1)
        {
            // ����
            moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        }
        else
        {
            moveDir = ((Vector2)transform.position - (Vector2)targetPos).normalized;
        }

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);

    }



    public override void Զ�̹���()
    {
        animator.ResetTrigger("attack");
        if (!isDive)
        {
            if (diveClk <= 0)
            {
                isDive = true;
                
                animator.SetBool("isDiving", true);
                diveClk = dive��ȴʱ��;
            }
        }
        else
        {
            isDive = false;
            awayFlag = 0;
            animator.SetBool("isDiving", false);
        }
    }

    public void �ؽ�����()
    {
        GameObjectUtil.SetAllChildGameObjectEnable(transform, false);
        isDead = true;
    }

    public void ����()
    {
        UItrans.gameObject.SetActive(true);
        transform.Find("Core").gameObject.SetActive(true);
        transform.Find("AttackTrigger").gameObject.SetActive(true);
        transform.Find("MoveTrigger").gameObject.SetActive(true);
        isDead = false;
    }

    public override void ����()
    {
        // ��ת����
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
    }




}
