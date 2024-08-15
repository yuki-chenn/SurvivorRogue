using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 妖怪书 : Enemy
{

    public float changeDirectionInterval = 2f; // 改变方向的时间间隔
    private Vector2 wanderDirection; // 游荡方向
    private float changeDirectionTimer = -1f; // 计时器

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

    public override void 近战攻击()
    {
        animator.SetBool("isLock", false);
        animator.SetTrigger("attack");
    }

    public override void 移动()
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
        rigidBody.AddForce(moveDir * attr.移动速度 * 8 * Time.deltaTime * 20);
    }

    public override void 待机()
    {
        animator.ResetTrigger("attack");
        animator.SetBool("isLock", false);
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            wanderDirection = RandomUtil.RandomDirection();
            changeDirectionTimer = changeDirectionInterval;
        }
        // 随机游荡
        var moveDir = wanderDirection;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20);
    }




}
