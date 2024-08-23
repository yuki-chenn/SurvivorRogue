using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ������ : Enemy
{

    public float changeDirectionInterval = 2f; // �ı䷽���ʱ����
    private Vector2 wanderDirection; // �ε�����
    private float changeDirectionTimer = -1f; // ��ʱ��

    public GameObject ����;
    public float ��ת�ٶ� = 15.0f; // ����ɨ���ٶ�
    public float �����ת�Ƕ� = 60.0f; // ����ɨ��Ƕȷ�Χ


    protected override void Start()
    {
        base.Start();
        wanderDirection = RandomUtil.RandomDirection();
        changeDirectionTimer = changeDirectionInterval;
    }

    protected override void Update()
    {
        base.Update();

    }

    public override void Զ�̹���()
    {
        animator.SetBool("isAttack", true);
    }

    public void ���伤��()
    {
        if (���� == null)
        {
            return;
        }
        ����.SetActive(true);
        // �����ü����׼���
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isLock", false);
            ����.SetActive(false); // Ŀ�겻����ʱ�رռ���
            return;
        }

        // ���㼤�ⷽ��
        Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (transform.localScale.x > 0) angle -= 180f;
        ����.transform.rotation = Quaternion.Euler(0, 0, angle);

        // ����׷��
        StartCoroutine(����׷��());
    }

    public void �رռ���()
    {
        if(���� != null) ����.SetActive(false);
        StopCoroutine(����׷��());
    }

    IEnumerator ����׷��()
    {
        while (true)
        {
            // ��ȡ���λ��
            Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
            if (targetPos == null)
            {
                ����.SetActive(false); // Ŀ�겻����ʱ�رռ���
                yield break;
            }

            // ����Ŀ�귽��
            Vector2 direction = ((Vector2)targetPos - (Vector2)transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (transform.localScale.x > 0) targetAngle -= 180f;

            // ���㵱ǰ�������ת�Ƕ�
            float currentAngle = ����.transform.rotation.eulerAngles.z;

            // ��ֵ��ת
            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, ��ת�ٶ� * Time.deltaTime);
            //Debug.Log("dir:" + direction + " cur:" + currentAngle + " tar:" + targetAngle);
            // ������ת�Ƕ�
            float initialAngle = transform.rotation.eulerAngles.z;
            float angleDifference = Mathf.DeltaAngle(initialAngle, newAngle);

            if (Mathf.Abs(angleDifference) > �����ת�Ƕ�)
            {
                newAngle = initialAngle + Mathf.Sign(angleDifference) * �����ת�Ƕ�;
            }

            // ���¼������ת�Ƕ�
            ����.transform.rotation = Quaternion.Euler(0, 0, newAngle);

            yield return null; // �ȴ���һ֡
        }
    }

    public override void ����()
    {
        �رռ���();
        animator.SetBool("isAttack", false);
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            wanderDirection = RandomUtil.RandomDirection();
            changeDirectionTimer = changeDirectionInterval;
        }
        // ����ε�
        var moveDir = wanderDirection;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);
    }

    public override void Die(float time)
    {
        �رռ���();
        StopAllCoroutines();
        Destroy(����);
        base.Die(time);
    }


}
