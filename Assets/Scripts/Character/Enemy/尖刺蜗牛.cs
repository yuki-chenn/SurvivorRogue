using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 尖刺蜗牛 : Enemy
{
    public GameObject 发射物体;
    public Transform 发射物体生成点;




    protected override void Update()
    {
        base.Update();
    }

    public override void 远程攻击()
    {
        animator.SetTrigger("attack");
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

    public override void 待机()
    {
        animator.SetBool("isMoving", false);
    }


    public void Throw()
    {
        // 向上下左右四个方向发射尖刺
        var directions = Constants.DIR4;
        for (int i = 0; i < 4; ++i)
        {
            Vector2 direction = directions[i];
            // 计算旋转角度
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            var spike = GameObject.Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
            spike.GetComponent<飞行物伤害>().source = gameObject;
            spike.GetComponent<直线飞行>().StartMove(direction);
        }

    }



}
