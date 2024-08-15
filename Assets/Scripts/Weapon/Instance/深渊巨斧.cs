using System;
using System.Collections;
using System.Collections.Generic;
using Survivor.Template;
using UnityEngine;
using DG.Tweening;


// 攻击产生波动，对大范围内的所有敌人造成伤害，并击退周围的敌人
class 深渊巨斧 : BaseWeapon
{
    public GameObject 攻击物;

    public float 生命倍率 = 0.2f;

    public override float 攻击力 => weaponInfo.Attck +
        生命倍率 * GameManager.Instance.Player.attr.最大生命;

    public float attackDuration
    {
        get
        {
            return Mathf.Min(攻击间隔 - 0.1f, 1f);
        }
    }

    public bool 一圈攻击 = false;
    public float scale = 2;

    protected override void Start()
    {
        base.Start();
        攻击物.GetComponent<武器伤害>().source = GameManager.Instance.playerGo;
        攻击物.GetComponent<武器伤害>().weapon = this;
        攻击物.SetActive(false);
    }

    protected override void 攻击()
    {
        if (enemyList.Count == 0) return;
        Transform target = 锁定目标();
        if (target == null) return;

        攻击间隔clk = 攻击间隔;

        int sign = ((Vector2)(target.position - transform.position)).IsLeftOf(Vector2.up) ? 1 : -1;
        transform.GetComponent<SpriteRenderer>().flipX = sign < 0;

        // 变大之前把collider打开，trigger关了
        circleCollider.enabled = false;
        攻击物.SetActive(true);

        // 先要变大
        transform.DOScale(scale, attackDuration / 5).OnComplete(()=> 
        {
            // 以target为中心的半圆扇面进行旋转
            if (一圈攻击) 旋转一圈(sign);
            else 旋转半圆扇面(target, sign);
        });

    }


    private void 旋转半圆扇面(Transform target,int sign)
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

        // 设置旋转范围为±90度
        float minAngle = targetAngle + sign * 89f;
        float maxAngle = targetAngle - sign * 89f;

        transform.rotation = Quaternion.Euler(0, 0, maxAngle);

        // 从minAngle旋转到maxAngle
        Sequence rotationSequence = DOTween.Sequence();
        rotationSequence.Append(transform.DORotate(new Vector3(0, 0, minAngle), attackDuration / 5 * 3 / 5 * 4).SetEase(Ease.InQuart))
                         .Append(transform.DORotate(new Vector3(0, 0, 0), attackDuration / 5 * 3 / 5 * 1))
                         .OnComplete(恢复大小);
    }

    private void 旋转一圈(int sign)
    {
        transform.DORotate(new Vector3(0, 0, sign * 360), attackDuration / 5 * 3, RotateMode.FastBeyond360)
             .SetEase(Ease.InQuart).OnComplete(()=> { 恢复大小(); transform.rotation = Quaternion.Euler(0, 0, 0); });
    }

    private void 恢复大小()
    {
        // 恢复原来的大小
        transform.DOScale(2, attackDuration / 5);
        circleCollider.enabled = true;
        攻击物.SetActive(false);
    }


    protected Transform 锁定目标()
    {
        // 默认选择最近的
        float minDis = float.MaxValue;
        Transform ret = null;
        foreach (var trans in enemyList)
        {
            var dis = Vector3.Distance(trans.position, transform.position);
            if (dis < minDis)
            {
                minDis = dis;
                ret = trans;
            }
        }
        return ret;
    }
}

