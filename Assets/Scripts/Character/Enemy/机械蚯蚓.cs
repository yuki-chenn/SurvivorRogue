using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 机械蚯蚓 : Enemy
{
    public float dive冷却时间 = 5f;
    private float diveClk;

    public bool isDive = false;
    private int awayFlag = 0; // 0表示不知道，1表示靠近，-1表示远离

    public float disThreshold = 1f;


    protected override void Start()
    {
        base.Start();
        diveClk = dive冷却时间;
    }

    protected override void Update()
    {
        base.Update();
        if (!isDive)
        {
            diveClk -= Time.deltaTime;
        }

    }

    public override void 近战攻击()
    {
        // 先转方向
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        // 再攻击
        animator.SetTrigger("attack");
    }

    public override void 移动()
    {
        
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }
        // 计算一下和Player的距离
        float dis = Vector2.Distance((Vector2)targetPos, (Vector2)transform.position);

        Vector2 moveDir = Vector2.zero;
        if (awayFlag == 0)
        {
            awayFlag = dis >= disThreshold ? 1 : -1;
        }

        if(awayFlag == 1)
        {
            // 靠近
            moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        }
        else
        {
            moveDir = ((Vector2)transform.position - (Vector2)targetPos).normalized;
        }

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20);

    }



    public override void 远程攻击()
    {
        animator.ResetTrigger("attack");
        if (!isDive)
        {
            if (diveClk <= 0)
            {
                isDive = true;
                
                animator.SetBool("isDiving", true);
                diveClk = dive冷却时间;
            }
        }
        else
        {
            isDive = false;
            awayFlag = 0;
            animator.SetBool("isDiving", false);
        }
    }

    public void 藏进地下()
    {
        GameObjectUtil.SetAllChildGameObjectEnable(transform, false);
        isDead = true;
    }

    public void 跳出()
    {
        UItrans.gameObject.SetActive(true);
        transform.Find("Core").gameObject.SetActive(true);
        transform.Find("AttackTrigger").gameObject.SetActive(true);
        transform.Find("MoveTrigger").gameObject.SetActive(true);
        isDead = false;
    }

    public override void 待机()
    {
        // 先转方向
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
