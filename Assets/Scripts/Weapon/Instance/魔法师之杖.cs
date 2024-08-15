using System;
using System.Collections;
using System.Collections.Generic;
using Survivor.Template;
using UnityEngine;
using DG.Tweening;


// 可以同时对攻击范围内的最多n名敌人造成伤害。
class 魔法师之杖 : BaseWeapon
{

    public GameObject 攻击飞行物;
    public Transform 攻击物生成点;

    public int 同时攻击的人数 = 2;
    public float 智力倍率 = 0.2f;

    public override float 攻击力 => weaponInfo.Attck + 智力倍率 * GameManager.Instance.Player.attr.智力;

    private float swingAngle = 10f; // 摇摆的角度范围
    private float swingDuration = 0.2f; // 摇摆动画的持续时间


    protected override void 攻击()
    {
        //Debug.Log("攻击:" + 攻击间隔);
        if (enemyList.Count == 0) return;
        List<Transform> targets = 锁定目标(同时攻击的人数);
        if (targets == null) return;

        攻击间隔clk = 攻击间隔;

        // 发射
        WandAnimation();
        发射(targets);

    }

    private void WandAnimation()
    {
        // 创建一个序列来控制摇摆动画
        Sequence swingSequence = DOTween.Sequence();

        // 法杖向左摇摆
        swingSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -swingAngle), swingDuration / 2));

        // 法杖向右摇摆
        swingSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, swingAngle), swingDuration / 2));

        // 回到原始角度
        swingSequence.Append(transform.DOLocalRotate(Vector3.zero, swingDuration / 2));
    }


    protected void 发射(List<Transform> targets)
    {
        for(int i = 0; i < targets.Count; ++i)
        {
            var magic = Instantiate(攻击飞行物, 攻击物生成点.position, Quaternion.identity, ContainerManager.Instance.weaponObjectContainer);
            magic.GetComponent<武器伤害>().weapon = this;
            magic.GetComponent<武器伤害>().source = GameManager.Instance.playerGo;

            // weapon.transform.localScale = GeometryUtil.GetDirectionScale(head.transform.localScale, direction, "x");
            magic.GetComponent<FlyingObject>().StartMove(targets[i]);
        }
        
    }

    protected List<Transform> 锁定目标(int n)
    {
        // 存储距离和目标的配对
        List<(float distance, Transform transform)> distanceList = new List<(float, Transform)>();

        // 计算每个目标的距离并添加到列表中
        foreach (var trans in enemyList)
        {
            var dis = Vector3.Distance(trans.position, transform.position);
            distanceList.Add((dis, trans));
        }

        // 按距离排序
        distanceList.Sort((a, b) => a.distance.CompareTo(b.distance));

        // 取最近的n个目标
        List<Transform> result = new List<Transform>();
        for (int i = 0; i < n && i < distanceList.Count; i++)
        {
            result.Add(distanceList[i].transform);
        }

        return result;
    }

}

