using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ����Ⱥ : Enemy
{

    protected override void Update()
    {
        base.Update();
    }

    public override void �ƶ�()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);
    }
}
