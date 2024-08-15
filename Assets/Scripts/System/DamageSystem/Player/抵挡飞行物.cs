using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 玩家所发出或持有的攻击物体，只对所有非玩家角色碰撞
public class 抵挡飞行物 : BaseAttackObject 
{

    public List<string> 抵挡tag;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (抵挡tag.Contains(collision.tag))
        {
            Destroy(collision.gameObject);
        }

    }
}
