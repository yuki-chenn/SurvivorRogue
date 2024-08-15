using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ����׳�Boss : Enemy
{
    public float ���ʱ�� = 1f;
    public GameObject ��й�������;

    public float ���������ʱ�� = 10f;
    public int ����������� = 5;

    public float ��̷ɵ�ʱ�� = 5f;
    public int ��̷ɵ����� = 5;
    public int ��̷ɵ����� = 20;

    public GameObject ��������;
    public Transform �����������ɵ�;

    public float ̩ɽѹ��ʱ�� = 2.0f;
    public float ̩ɽѹ��Ծ��߶� = 5.0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (Զ��clk > 0) Զ��clk -= Time.deltaTime;
    }

    public override void �ƶ�()
    {
        animator.SetBool("isMoving", true);
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;

        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20 * rigidBody.mass);
    }

    public override void ����()
    {
        animator.SetBool("isMoving", false);
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            return;
        }

        Vector2 moveDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
    }

    #region �������

    public void �������()
    {
        animator.SetBool("isRolling", true);
    }

    public void ��ʼ�������()
    {
        StartCoroutine(Coroutine�������());
        transform.Find("RollCollider").gameObject.SetActive(true);
        transform.Find("DustFX").gameObject.SetActive(true);
        //transform.Find("Core").gameObject.SetActive(false);
    }

    IEnumerator Coroutine�������()
    {
        float ÿ�ι���ʱ�� = ���������ʱ�� / �����������;
        float timer = 0;
        int cnt = 0;

        Vector2 rollDir = Vector2.zero;

        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = ÿ�ι���ʱ��;
                cnt++;
                Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
                if (targetPos == null)
                {
                    break;
                }

                rollDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
            }

            if (cnt > �����������) break;

            transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, rollDir, "x");
            UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, rollDir, "x");
            rigidBody.AddForce(rollDir * attr.�ƶ��ٶ� * 8 * Time.deltaTime * 20 * rigidBody.mass);
            yield return null;
        }


        animator.SetBool("isRolling", false);
    }

    #endregion

    #region ���
    public void ���()
    {
        animator.SetBool("isRoaring", true);
        StartCoroutine(Coroutine���(���ʱ��));

    }

    public void ��ʾ���Effect()
    {
        ��й�������.SetActive(true);
    }

    IEnumerator Coroutine���(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("isRoaring", false);
    }

    #endregion

    #region ��̷ɵ�

    public void ��̷ɵ�()
    {
        animator.SetBool("isSpiking", true);
    }

    public void ��ʼ��̷ɵ�()
    {
        StartCoroutine(Coroutine��̷ɵ�());
    }

    IEnumerator Coroutine��̷ɵ�()
    {
        float interval = ��̷ɵ�ʱ�� / ��̷ɵ�����;
        // ���� ��̷ɵ����� �ι�����ÿ�ι�������Χ����һȦ ��̷ɵ����� ��spike��ÿ�ι������interval����ÿ�ι������ɵķɵ�λ�ô�
        float angleStep = 360f / ��̷ɵ�����;

        for (int i = 0; i < ��̷ɵ�����; i++)
        {
            int offset = RandomUtil.RandomInt(2, 4);
            for (int j = 0; j < ��̷ɵ�����; j++)
            {
                float angle = j * angleStep + (i * angleStep / offset);
                Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

                var spike = GameObject.Instantiate(��������, �����������ɵ�.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);

                spike.GetComponent<�������˺�>().source = gameObject;
                spike.GetComponent<ֱ�߷���>().StartMove(direction);
            }

            yield return new WaitForSeconds(interval);
        }

        animator.SetBool("isSpiking", false);
    }

    #endregion

    #region ̩ɽѹ��
    public void ̩ɽѹ��()
    {
        animator.SetTrigger("fly");
        StartCoroutine(Coroutine̩ɽѹ��());
    }

    IEnumerator Coroutine̩ɽѹ��()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            yield break;
        }

        Vector2 startPos = transform.position;
        Vector2 endPos = (Vector2)targetPos;

        float elapsed = 0f;

        while (elapsed < ̩ɽѹ��ʱ��)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / ̩ɽѹ��ʱ��;

            // �������Һ����켣
            float sinValue = Mathf.Sin(t * Mathf.PI);
            float heightOffset = ̩ɽѹ��Ծ��߶� * sinValue;
            Vector2 currentPos = Vector2.Lerp(startPos, endPos, t);
            currentPos.y += heightOffset;

            transform.position = currentPos;

            if (t >= 0.5f && t <= 0.6f)
            {
                // �ڴﵽ��ߵ�ʱ����fall����
                animator.SetTrigger("fall");
            }

            yield return null;
        }

        // ��غ󴥷�land����
        animator.SetTrigger("land");
        transform.Find("landEF").gameObject.SetActive(true);
    }
    #endregion

    public void ���ܽ���()
    {
        ��й�������.SetActive(false);
        transform.Find("RollCollider").gameObject.SetActive(false);
        transform.Find("DustFX").gameObject.SetActive(false);
        //transform.Find("Core").gameObject.SetActive(true);
        animator.ResetTrigger("fly");
        animator.ResetTrigger("fall");
        animator.ResetTrigger("land");
        transform.Find("landEF").gameObject.SetActive(false);
        Զ��clk = Զ����ȴʱ��;
    }


}
