using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 机械枪兵 : Enemy
{
    public GameObject 发射物体;
    public Transform 发射物体生成点;
    public GameObject 攻击特效;


    protected override void Update()
    {
        base.Update();
        if(远程clk > 0) 远程clk -= Time.deltaTime;
    }

    public override void 远程攻击()
    {
        // 面向玩家
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }
        Vector2 dir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, dir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, dir, "x");

        animator.SetTrigger("attack");
        攻击特效.SetActive(true);
        int type = RandomUtil.RandomInt(0, 2);
        StartCoroutine(Throw(type));
    }

    IEnumerator Throw(int type)
    {
        switch (type)
        {
            case 0:
                // 向目标方向发射10发子弹
                Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
                if (targetPos == null)
                {
                    break;
                }

                for(int i = 0; i < 10; ++i)
                {
                    Vector2 direction = ((Vector2)targetPos - (Vector2)发射物体生成点.position).normalized;
                    var bullet = Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    bullet.GetComponent<飞行物伤害>().source = gameObject;
                    bullet.transform.localScale = GeometryUtil.GetDirectionScale(bullet.transform.localScale, direction, "x");
                    bullet.GetComponent<直线飞行>().StartMove(direction);
                    yield return new WaitForSeconds(0.2f);
                }
                break;
            case 1:
                // 向目标并且两侧45度方向发射5发子弹
                Vector2? targetPos1 = GameManager.Instance.GetPlayerPosition();
                if (targetPos1 == null)
                {
                    break;
                }

                Vector2 midDirection = ((Vector2)targetPos1 - (Vector2)发射物体生成点.position).normalized;
                Vector2 leftDirection = Quaternion.Euler(0, 0, 45) * midDirection;
                Vector2 rightDirection = Quaternion.Euler(0, 0, -45) * midDirection;

                for (int i = 0; i < 5; ++i)
                {
                    var midBullet = Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    midBullet.GetComponent<飞行物伤害>().source = gameObject;
                    midBullet.transform.localScale = GeometryUtil.GetDirectionScale(midBullet.transform.localScale, midDirection, "x");
                    midBullet.GetComponent<直线飞行>().StartMove(midDirection);

                    var leftBullet = Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    leftBullet.GetComponent<飞行物伤害>().source = gameObject;
                    leftBullet.transform.localScale = GeometryUtil.GetDirectionScale(leftBullet.transform.localScale, leftDirection, "x");
                    leftBullet.GetComponent<直线飞行>().StartMove(leftDirection);

                    var rightBullet = Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    rightBullet.GetComponent<飞行物伤害>().source = gameObject;
                    rightBullet.transform.localScale = GeometryUtil.GetDirectionScale(rightBullet.transform.localScale, rightDirection, "x");
                    rightBullet.GetComponent<直线飞行>().StartMove(rightDirection);

                    yield return new WaitForSeconds(0.2f);
                }

                

                break;
            case 2:
                // 波浪扫射30发子弹
                Vector2? targetPos2 = GameManager.Instance.GetPlayerPosition();
                if (targetPos2 == null)
                {
                    break;
                }

                for (int i = 0; i < 30; ++i)
                {
                    float angle = Mathf.Sin(Time.time * 5) * 30; // 生成波浪效果的角度
                    Vector2 direction2 = ((Vector2)targetPos2 - (Vector2)发射物体生成点.position).normalized;
                    Vector2 waveDirection = Quaternion.Euler(0, 0, angle) * direction2;

                    var bullet2 = Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    bullet2.GetComponent<飞行物伤害>().source = gameObject;
                    bullet2.transform.localScale = GeometryUtil.GetDirectionScale(bullet2.transform.localScale, waveDirection, "x");
                    bullet2.GetComponent<直线飞行>().StartMove(waveDirection);
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            default:
                break;
        }
        远程clk = 远程冷却时间;
        animator.SetBool("isMoving", false);
        攻击特效.SetActive(false);
    }

    public override void 待机()
    {
        animator.SetBool("isMoving", false);
        animator.ResetTrigger("attack");
    }

    public override void Die(float time)
    {
        StopAllCoroutines();
        base.Die(time);
    }

    public override void 移动()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isMoving", false);
            return;
        }
        //Debug.LogError("tarpos:" + targetPos);
        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        animator.SetBool("isMoving", true);

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20);
    }



}
