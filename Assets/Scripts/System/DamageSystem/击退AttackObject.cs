using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敌人所发出或持有的攻击物体，只对玩家角色碰撞
public class 击退AttackObject : BaseAttackObject 
{
    private Vector2 dir;

    public List<string> collideTag;

    public float 击退力度;

    public bool 水平垂直移动 = false;

    public bool useParentPos = true;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collideTag.Contains(collision.tag))
        {
            //Debug.Log("击退");
            dir = (collision.transform.position - (useParentPos ? transform.parent.position : transform.position)).normalized;
            if (水平垂直移动)
            {
                // 将dir就近移动到水平或垂直的方向上
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
                {
                    dir = new Vector2(Mathf.Sign(dir.x), 0);
                }
                else
                {
                    dir = new Vector2(0, Mathf.Sign(dir.y));
                }
            }

            collision.GetComponentInParent<Rigidbody2D>().AddForce(dir * Time.deltaTime * 击退力度 * 20000);
        }

    }
}
