using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ģ������ : Enemy
{

    public float changeDirectionInterval = 2f; // �ı䷽���ʱ����
    private Vector2 wanderDirection; // �ε�����
    private float changeDirectionTimer = -1f; // ��ʱ��
    private bool isWandering = true;
    public float idleTime = 2f; // idle��ʱ��
    private float idleTimer = -1f; //��ʱ�� 

    private bool isFinded = false;
    public float escapeTime = 10f; // �������ʱ��
    private float escapeTimer = 0f;

    public bool bombSelf = false;

    protected override void Start()
    {
        base.Start();
        wanderDirection = RandomUtil.RandomDirection();
        changeDirectionTimer = changeDirectionInterval;
    }

    protected override void Update()
    {
        base.Update();

        if (isFinded && !isDead && !bombSelf)
        {
            escapeTimer += Time.deltaTime;
            if (escapeTimer > escapeTime)
            {
                bombSelf = true;
            }
        }
    }

    public void ��ը��������()
    {
        transform.Find("Core").gameObject.SetActive(false);
        transform.Find("EscapeTrigger").gameObject.SetActive(false);
        UItrans.gameObject.SetActive(false);
    }

    public override void ��ս����()
    {
        if (!bombSelf) return;
        animator.SetTrigger("attack");
        Destroy(gameObject,10.0f);
    }

    public override void �ƶ�()
    {
        isFinded = true;

        animator.SetBool("isMoving", true);
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isMoving", false);
            return;
        }
        
        Vector2 moveDir = ((Vector2)transform.position - (Vector2)targetPos).normalized;

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * 3 * Time.deltaTime * 20);
    }

    public override void ����()
    {

        if (isWandering)
        {
            animator.SetBool("isMoving", true);
            // ���������ε�
            changeDirectionTimer -= Time.deltaTime;
            // ����ε�
            var moveDir = wanderDirection;
            transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
            UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
            rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);

            if (changeDirectionTimer <= 0)
            {
                isWandering = false;
                wanderDirection = RandomUtil.RandomDirection();
                changeDirectionTimer = changeDirectionInterval;
            }

        }
        else
        {
            animator.SetBool("isMoving", false);
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                isWandering = true;
                idleTimer = idleTime;
            }
        }
    }




}
