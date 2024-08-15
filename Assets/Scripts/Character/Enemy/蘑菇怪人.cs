using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 蘑菇怪人 : Enemy
{

    public float changeDirectionInterval = 2f; // 改变方向的时间间隔
    private Vector2 wanderDirection; // 游荡方向
    private float changeDirectionTimer = -1f; // 计时器
    private bool isWandering = true;
    public float idleTime = 2f; // idle的时间
    private float idleTimer = -1f; //计时器 

    private bool isFinded = false;
    public float escapeTime = 10f; // 最大逃跑时间
    private float escapeTimer = 0f;

    public bool bombSelf = false;

    protected override void Start()
    {
        base.Start();
        wanderDirection = RandomUtil.RandomDirection();
        changeDirectionTimer = changeDirectionInterval;
    }

    protected override void Update()
    {
        base.Update();

        if (isFinded && !isDead && !bombSelf)
        {
            escapeTimer += Time.deltaTime;
            if (escapeTimer > escapeTime)
            {
                bombSelf = true;
            }
        }
    }

    public void 爆炸自身死亡()
    {
        transform.Find("Core").gameObject.SetActive(false);
        transform.Find("EscapeTrigger").gameObject.SetActive(false);
        UItrans.gameObject.SetActive(false);
    }

    public override void 近战攻击()
    {
        if (!bombSelf) return;
        animator.SetTrigger("attack");
        Destroy(gameObject,10.0f);
    }

    public override void 移动()
    {
        isFinded = true;

        animator.SetBool("isMoving", true);
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isMoving", false);
            return;
        }
        
        Vector2 moveDir = ((Vector2)transform.position - (Vector2)targetPos).normalized;

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.移动速度 * 3 * Time.deltaTime * 20);
    }

    public override void 待机()
    {

        if (isWandering)
        {
            animator.SetBool("isMoving", true);
            // 现在正在游荡
            changeDirectionTimer -= Time.deltaTime;
            // 随机游荡
            var moveDir = wanderDirection;
            transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
            UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
            rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20);

            if (changeDirectionTimer <= 0)
            {
                isWandering = false;
                wanderDirection = RandomUtil.RandomDirection();
                changeDirectionTimer = changeDirectionInterval;
            }

        }
        else
        {
            animator.SetBool("isMoving", false);
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                isWandering = true;
                idleTimer = idleTime;
            }
        }
    }




}
