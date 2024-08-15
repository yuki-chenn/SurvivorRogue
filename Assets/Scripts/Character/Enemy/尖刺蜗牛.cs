using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class �����ţ : Enemy
{
    public GameObject ��������;
    public Transform �����������ɵ�;




    protected override void Update()
    {
        base.Update();
    }

    public override void Զ�̹���()
    {
        animator.SetTrigger("attack");
    }

    public override void �ƶ�()
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
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);
    }

    public override void ����()
    {
        animator.SetBool("isMoving", false);
    }


    public void Throw()
    {
        // �����������ĸ���������
        var directions = Constants.DIR4;
        for (int i = 0; i < 4; ++i)
        {
            Vector2 direction = directions[i];
            // ������ת�Ƕ�
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            var spike = GameObject.Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
            spike.GetComponent<�������˺�>().source = gameObject;
            spike.GetComponent<ֱ�߷���>().StartMove(direction);
        }

    }



}
