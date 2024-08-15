using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ���ù� : Enemy
{
    public GameObject ��������;
    public Transform �����������ɵ�;

    


    protected override void Update()
    {
        base.Update();
        if (��սclk > 0) ��սclk -= Time.deltaTime;
        if (Զ��clk > 0) Զ��clk -= Time.deltaTime;
    }

    public override void ��ս����()
    {
        if (��սclk > 0) return;
        ��սclk = ��ս��ȴʱ��;

        animator.SetTrigger("attack");
        //animator.SetBool("isMoving", false);
    }

    public override void Զ�̹���()
    {
        if (Զ��clk > 0) return;
        Զ��clk = Զ����ȴʱ��;

        animator.SetTrigger("throw");
        //animator.SetBool("isMoving", false);
    }

    public override void �ƶ�()
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
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);
    }


    public void Throw()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        var head = GameObject.Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
        head.GetComponent<�������˺�>().source = gameObject;

        head.transform.localScale = GeometryUtil.GetDirectionScale(head.transform.localScale, direction, "x");
        head.GetComponent<ֱ�߷���>().StartMove(direction);
    }



}
