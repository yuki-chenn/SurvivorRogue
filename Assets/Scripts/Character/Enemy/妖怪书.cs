using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ������ : Enemy
{

    public float changeDirectionInterval = 2f; // �ı䷽���ʱ����
    private Vector2 wanderDirection; // �ε�����
    private float changeDirectionTimer = -1f; // ��ʱ��

    protected override void Start()
    {
        base.Start();
        wanderDirection = RandomUtil.RandomDirection();
        changeDirectionTimer = changeDirectionInterval;
    }

    protected override void Update()
    {
        base.Update();
        
    }

    public override void ��ս����()
    {
        animator.SetBool("isLock", false);
        animator.SetTrigger("attack");
    }

    public override void �ƶ�()
    {
        animator.ResetTrigger("attack");
        animator.SetBool("isLock", true);
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isLock", false);
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * 8 * Time.deltaTime * 20);
    }

    public override void ����()
    {
        animator.ResetTrigger("attack");
        animator.SetBool("isLock", false);
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            wanderDirection = RandomUtil.RandomDirection();
            changeDirectionTimer = changeDirectionInterval;
        }
        // ����ε�
        var moveDir = wanderDirection;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);
    }




}
