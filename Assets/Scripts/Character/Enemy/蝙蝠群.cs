using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 蝙蝠群 : Enemy
{

    protected override void Update()
    {
        base.Update();
    }

    public override void 移动()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20);
    }
}
