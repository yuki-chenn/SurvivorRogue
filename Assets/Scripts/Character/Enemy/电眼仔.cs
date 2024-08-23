using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 电眼仔 : Enemy
{

    public float changeDirectionInterval = 2f; // 改变方向的时间间隔
    private Vector2 wanderDirection; // 游荡方向
    private float changeDirectionTimer = -1f; // 计时器

    public GameObject 激光;
    public float 旋转速度 = 15.0f; // 控制扫射速度
    public float 最大旋转角度 = 60.0f; // 控制扫射角度范围


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

    public override void 远程攻击()
    {
        animator.SetBool("isAttack", true);
    }

    public void 发射激光()
    {
        if (激光 == null)
        {
            return;
        }
        激光.SetActive(true);
        // 先设置激光对准玩家
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isLock", false);
            激光.SetActive(false); // 目标不存在时关闭激光
            return;
        }

        // 计算激光方向
        Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (transform.localScale.x > 0) angle -= 180f;
        激光.transform.rotation = Quaternion.Euler(0, 0, angle);

        // 激光追踪
        StartCoroutine(激光追踪());
    }

    public void 关闭激光()
    {
        if(激光 != null) 激光.SetActive(false);
        StopCoroutine(激光追踪());
    }

    IEnumerator 激光追踪()
    {
        while (true)
        {
            // 获取玩家位置
            Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
            if (targetPos == null)
            {
                激光.SetActive(false); // 目标不存在时关闭激光
                yield break;
            }

            // 计算目标方向
            Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (transform.localScale.x > 0) targetAngle -= 180f;

            // 计算当前激光的旋转角度
            float currentAngle = 激光.transform.rotation.eulerAngles.z;

            // 插值旋转
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, 旋转速度 * Time.deltaTime);
            //Debug.Log("dir:" + direction + " cur:" + currentAngle + " tar:" + targetAngle);
            // 限制旋转角度
            float initialAngle = transform.rotation.eulerAngles.z;
            float angleDifference = Mathf.DeltaAngle(initialAngle, newAngle);

            if (Mathf.Abs(angleDifference) > 最大旋转角度)
            {
                newAngle = initialAngle + Mathf.Sign(angleDifference) * 最大旋转角度;
            }

            // 更新激光的旋转角度
            激光.transform.rotation = Quaternion.Euler(0, 0, newAngle);

            yield return null; // 等待下一帧
        }
    }

    public override void 待机()
    {
        关闭激光();
        animator.SetBool("isAttack", false);
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

    public override void Die(float time)
    {
        关闭激光();
        StopAllCoroutines();
        Destroy(激光);
        base.Die(time);
    }


}
