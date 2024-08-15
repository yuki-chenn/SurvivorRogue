using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ��еǹ�� : Enemy
{
    public GameObject ��������;
    public Transform �����������ɵ�;
    public GameObject ������Ч;


    protected override void Update()
    {
        base.Update();
        if(Զ��clk > 0) Զ��clk -= Time.deltaTime;
    }

    public override void Զ�̹���()
    {
        // �������
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }
        Vector2 dir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, dir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, dir, "x");

        animator.SetTrigger("attack");
        ������Ч.SetActive(true);
        int type = RandomUtil.RandomInt(0, 2);
        StartCoroutine(Throw(type));
    }

    IEnumerator Throw(int type)
    {
        switch (type)
        {
            case 0:
                // ��Ŀ�귽����10���ӵ�
                Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
                if (targetPos == null)
                {
                    break;
                }

                for(int i = 0; i < 10; ++i)
                {
                    Vector2 direction = ((Vector2)targetPos - (Vector2)�����������ɵ�.position).normalized;
                    var bullet = Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    bullet.GetComponent<�������˺�>().source = gameObject;
                    bullet.transform.localScale = GeometryUtil.GetDirectionScale(bullet.transform.localScale, direction, "x");
                    bullet.GetComponent<ֱ�߷���>().StartMove(direction);
                    yield return new WaitForSeconds(0.2f);
                }
                break;
            case 1:
                // ��Ŀ�겢������45�ȷ�����5���ӵ�
                Vector2? targetPos1 = GameManager.Instance.GetPlayerPosition();
                if (targetPos1 == null)
                {
                    break;
                }

                Vector2 midDirection = ((Vector2)targetPos1 - (Vector2)�����������ɵ�.position).normalized;
                Vector2 leftDirection = Quaternion.Euler(0, 0, 45) * midDirection;
                Vector2 rightDirection = Quaternion.Euler(0, 0, -45) * midDirection;

                for (int i = 0; i < 5; ++i)
                {
                    var midBullet = Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    midBullet.GetComponent<�������˺�>().source = gameObject;
                    midBullet.transform.localScale = GeometryUtil.GetDirectionScale(midBullet.transform.localScale, midDirection, "x");
                    midBullet.GetComponent<ֱ�߷���>().StartMove(midDirection);

                    var leftBullet = Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    leftBullet.GetComponent<�������˺�>().source = gameObject;
                    leftBullet.transform.localScale = GeometryUtil.GetDirectionScale(leftBullet.transform.localScale, leftDirection, "x");
                    leftBullet.GetComponent<ֱ�߷���>().StartMove(leftDirection);

                    var rightBullet = Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    rightBullet.GetComponent<�������˺�>().source = gameObject;
                    rightBullet.transform.localScale = GeometryUtil.GetDirectionScale(rightBullet.transform.localScale, rightDirection, "x");
                    rightBullet.GetComponent<ֱ�߷���>().StartMove(rightDirection);

                    yield return new WaitForSeconds(0.2f);
                }

                

                break;
            case 2:
                // ����ɨ��30���ӵ�
                Vector2? targetPos2 = GameManager.Instance.GetPlayerPosition();
                if (targetPos2 == null)
                {
                    break;
                }

                for (int i = 0; i < 30; ++i)
                {
                    float angle = Mathf.Sin(Time.time * 5) * 30; // ���ɲ���Ч���ĽǶ�
                    Vector2 direction2 = ((Vector2)targetPos2 - (Vector2)�����������ɵ�.position).normalized;
                    Vector2 waveDirection = Quaternion.Euler(0, 0, angle) * direction2;

                    var bullet2 = Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);
                    bullet2.GetComponent<�������˺�>().source = gameObject;
                    bullet2.transform.localScale = GeometryUtil.GetDirectionScale(bullet2.transform.localScale, waveDirection, "x");
                    bullet2.GetComponent<ֱ�߷���>().StartMove(waveDirection);
                    yield return new WaitForSeconds(0.1f);
                }
                break;
            default:
                break;
        }
        Զ��clk = Զ����ȴʱ��;
        animator.SetBool("isMoving", false);
        ������Ч.SetActive(false);
    }

    public override void ����()
    {
        animator.SetBool("isMoving", false);
        animator.ResetTrigger("attack");
    }

    public override void Die(float time)
    {
        StopAllCoroutines();
        base.Die(time);
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



}
