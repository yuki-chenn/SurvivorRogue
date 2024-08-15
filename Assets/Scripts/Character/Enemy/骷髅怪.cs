using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 骷髅怪 : Enemy
{
    public GameObject 发射物体;
    public Transform 发射物体生成点;

    


    protected override void Update()
    {
        base.Update();
        if (近战clk > 0) 近战clk -= Time.deltaTime;
        if (远程clk > 0) 远程clk -= Time.deltaTime;
    }

    public override void 近战攻击()
    {
        if (近战clk > 0) return;
        近战clk = 近战冷却时间;

        animator.SetTrigger("attack");
        //animator.SetBool("isMoving", false);
    }

    public override void 远程攻击()
    {
        if (远程clk > 0) return;
        远程clk = 远程冷却时间;

        animator.SetTrigger("throw");
        //animator.SetBool("isMoving", false);
    }

    public override void 移动()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        animator.SetBool("isMoving", true);

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20);
    }


    public void Throw()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        var head = GameObject.Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
        head.GetComponent<飞行物伤害>().source = gameObject;

        head.transform.localScale = GeometryUtil.GetDirectionScale(head.transform.localScale, direction, "x");
        head.GetComponent<直线飞行>().StartMove(direction);
    }



}
