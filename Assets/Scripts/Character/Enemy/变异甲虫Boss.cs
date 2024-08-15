using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 变异甲虫Boss : Enemy
{
    public float 吼叫时间 = 1f;
    public GameObject 吼叫攻击物体;

    public float 滚动冲击总时间 = 10f;
    public int 滚动冲击次数 = 5;

    public float 尖刺飞弹时间 = 5f;
    public int 尖刺飞弹次数 = 5;
    public int 尖刺飞弹个数 = 20;

    public GameObject 发射物体;
    public Transform 发射物体生成点;

    public float 泰山压顶时间 = 2.0f;
    public float 泰山压顶跃起高度 = 5.0f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (远程clk > 0) 远程clk -= Time.deltaTime;
    }

    public override void 移动()
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
        rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20 * rigidBody.mass);
    }

    public override void 待机()
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

    #region 滚动冲击

    public void 滚动冲击()
    {
        animator.SetBool("isRolling", true);
    }

    public void 开始滚动冲击()
    {
        StartCoroutine(Coroutine滚动冲击());
        transform.Find("RollCollider").gameObject.SetActive(true);
        transform.Find("DustFX").gameObject.SetActive(true);
        //transform.Find("Core").gameObject.SetActive(false);
    }

    IEnumerator Coroutine滚动冲击()
    {
        float 每次滚动时间 = 滚动冲击总时间 / 滚动冲击次数;
        float timer = 0;
        int cnt = 0;

        Vector2 rollDir = Vector2.zero;

        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 每次滚动时间;
                cnt++;
                Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
                if (targetPos == null)
                {
                    break;
                }

                rollDir = ((Vector2)targetPos - (Vector2)transform.position).normalized;
            }

            if (cnt > 滚动冲击次数) break;

            transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, rollDir, "x");
            UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, rollDir, "x");
            rigidBody.AddForce(rollDir * attr.移动速度 * 8 * Time.deltaTime * 20 * rigidBody.mass);
            yield return null;
        }


        animator.SetBool("isRolling", false);
    }

    #endregion

    #region 吼叫
    public void 吼叫()
    {
        animator.SetBool("isRoaring", true);
        StartCoroutine(Coroutine吼叫(吼叫时间));

    }

    public void 显示吼叫Effect()
    {
        吼叫攻击物体.SetActive(true);
    }

    IEnumerator Coroutine吼叫(float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("isRoaring", false);
    }

    #endregion

    #region 尖刺飞弹

    public void 尖刺飞弹()
    {
        animator.SetBool("isSpiking", true);
    }

    public void 开始尖刺飞弹()
    {
        StartCoroutine(Coroutine尖刺飞弹());
    }

    IEnumerator Coroutine尖刺飞弹()
    {
        float interval = 尖刺飞弹时间 / 尖刺飞弹次数;
        // 生成 尖刺飞弹次数 次攻击，每次攻击在周围生成一圈 尖刺飞弹个数 个spike，每次攻击间隔interval，且每次攻击生成的飞弹位置错开
        float angleStep = 360f / 尖刺飞弹个数;

        for (int i = 0; i < 尖刺飞弹次数; i++)
        {
            int offset = RandomUtil.RandomInt(2, 4);
            for (int j = 0; j < 尖刺飞弹个数; j++)
            {
                float angle = j * angleStep + (i * angleStep / offset);
                Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);

                var spike = GameObject.Instantiate(发射物体, 发射物体生成点.position, Quaternion.identity, ContainerManager.Instance.enemyContainer);

                spike.GetComponent<飞行物伤害>().source = gameObject;
                spike.GetComponent<直线飞行>().StartMove(direction);
            }

            yield return new WaitForSeconds(interval);
        }

        animator.SetBool("isSpiking", false);
    }

    #endregion

    #region 泰山压顶
    public void 泰山压顶()
    {
        animator.SetTrigger("fly");
        StartCoroutine(Coroutine泰山压顶());
    }

    IEnumerator Coroutine泰山压顶()
    {
        Vector2? targetPos = GameManager.Instance.GetPlayerPosition();
        if (targetPos == null)
        {
            yield break;
        }

        Vector2 startPos = transform.position;
        Vector2 endPos = (Vector2)targetPos;

        float elapsed = 0f;

        while (elapsed < 泰山压顶时间)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 泰山压顶时间;

            // 计算正弦函数轨迹
            float sinValue = Mathf.Sin(t * Mathf.PI);
            float heightOffset = 泰山压顶跃起高度 * sinValue;
            Vector2 currentPos = Vector2.Lerp(startPos, endPos, t);
            currentPos.y += heightOffset;

            transform.position = currentPos;

            if (t >= 0.5f && t <= 0.6f)
            {
                // 在达到最高点时触发fall动画
                animator.SetTrigger("fall");
            }

            yield return null;
        }

        // 落地后触发land动画
        animator.SetTrigger("land");
        transform.Find("landEF").gameObject.SetActive(true);
    }
    #endregion

    public void 技能结束()
    {
        吼叫攻击物体.SetActive(false);
        transform.Find("RollCollider").gameObject.SetActive(false);
        transform.Find("DustFX").gameObject.SetActive(false);
        //transform.Find("Core").gameObject.SetActive(true);
        animator.ResetTrigger("fly");
        animator.ResetTrigger("fall");
        animator.ResetTrigger("land");
        transform.Find("landEF").gameObject.SetActive(false);
        远程clk = 远程冷却时间;
    }


}
