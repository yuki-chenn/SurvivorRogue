using System;
using System.Collections;
using Survivor.Template;
using UnityEngine;


// 每n次攻击，下一次就会笔直的向敌人射出，并穿透当前路径上的所有敌人。
class 勇者之剑 : BaseWeapon
{

    public GameObject 攻击飞行物;
    public GameObject 穿透攻击飞行物;

    public float 攻击倍率 = 0.2f;
    public int 穿透攻击次数 = 4;

    public override float 攻击力 => weaponInfo.Attck + 攻击倍率 * GameManager.Instance.Player.attr.力量;

    int count = 0;



    protected override void 攻击()
    {
        if (enemyList.Count == 0) return;
        Transform target = 锁定目标();
        if (target == null) return;

        攻击间隔clk = 攻击间隔;

        // 发射
        发射(target);

        // 隐藏自己
        StartCoroutine(DelayActive(Mathf.Max(攻击间隔 - 0.2f, 0.2f)));
    }

    IEnumerator DelayActive(float delay)
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<CircleCollider2D>().enabled = false;
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delay);
        // 启用对象
        transform.GetComponent<SpriteRenderer>().enabled = true;
        transform.GetComponent<CircleCollider2D>().enabled = true;
    }

    protected void 发射(Transform target)
    {
        GameObject weapon = null;
        if (count == 穿透攻击次数)
        {
            weapon = GameObject.Instantiate(穿透攻击飞行物, transform.position, Quaternion.identity, ContainerManager.Instance.weaponObjectContainer);
            weapon.GetComponent<武器伤害>().weapon = this;
            weapon.GetComponent<武器伤害>().source = GameManager.Instance.playerGo;
            count = 0;
        }
        else
        {
            weapon = GameObject.Instantiate(攻击飞行物, transform.position, Quaternion.identity, ContainerManager.Instance.weaponObjectContainer);
            weapon.GetComponent<武器伤害>().weapon = this;
            weapon.GetComponent<武器伤害>().source = GameManager.Instance.playerGo;
            count++;
        }
        
        
        // weapon.transform.localScale = GeometryUtil.GetDirectionScale(head.transform.localScale, direction, "x");
        weapon.GetComponent<FlyingObject>().StartMove(target);
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

