using System;
using System.Collections;
using System.Collections.Generic;
using Survivor.Template;
using UnityEngine;
using DG.Tweening;


// 攻击产生波动，对大范围内的所有敌人造成伤害，并击退周围的敌人
class 正义铁锤 : BaseWeapon
{

    public GameObject 攻击物;
    public Transform 攻击物生成点;

    public float 力量倍率 = 0.2f;
    public float 生命倍率 = 0.2f;

    public override float 攻击力 => weaponInfo.Attck +
        力量倍率 * GameManager.Instance.Player.attr.力量 +
        生命倍率 * GameManager.Instance.Player.attr.最大生命;

    public float swingDuration
    {
        get
        {
            return Mathf.Min(攻击间隔 - 0.1f, 1f);
        }
    }


    protected override void 攻击()
    {
        //Debug.Log("攻击:" + 攻击间隔);
        if (enemyList.Count == 0) return;
        Transform target = 锁定目标();
        if (target == null) return;

        攻击间隔clk = 攻击间隔;
        //controlled = false;

        int sign = ((Vector2)(target.position - transform.position)).IsLeftOf(Vector2.up) ? 1 : -1;
        transform.GetComponent<SpriteRenderer>().flipX = sign < 0;
        // 创建一个序列来控制摇摆动画
        Sequence swingSequence = DOTween.Sequence();
        // 反方向蓄力
        swingSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, sign * -45), swingDuration / 3).SetEase(Ease.InCubic));
        // 锤
        swingSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, sign * 90), swingDuration / 3).SetEase(Ease.OutCubic).OnComplete(锤击));

    }

    protected void 锤击()
    {

        var wave = Instantiate(攻击物, 攻击物生成点.position - new Vector3(0, 0.4f), Quaternion.identity, ContainerManager.Instance.weaponObjectContainer);
        wave.GetComponent<武器伤害>().weapon = this;
        wave.GetComponent<武器伤害>().source = GameManager.Instance.playerGo;

        transform.DOLocalRotate(new Vector3(0, 0, 0), swingDuration / 3); //.OnComplete(() => { controlled = true; });

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

