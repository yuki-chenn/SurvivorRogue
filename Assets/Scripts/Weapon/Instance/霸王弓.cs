using System;
using System.Collections;
using Survivor.Template;
using UnityEngine;
using DG.Tweening;

// 向前方射出n只箭矢
class 霸王弓 : BaseWeapon
{
    private Animator _ani;
    private Animator animator
    {
        get
        {
            if (_ani == null) _ani = transform.GetComponent<Animator>();
            return _ani;
        }
    }

    public GameObject 攻击飞行物;
    public Transform 攻击物生成点;

    public float angleOffset = 20f; // 每支箭的偏移角度

    public float 力量倍率 = 0.2f;
    public float 智力倍率 = 0.2f;

    public override float 攻击力 => weaponInfo.Attck +
        力量倍率 * GameManager.Instance.Player.attr.力量 +
        智力倍率 * GameManager.Instance.Player.attr.智力;

    public int arrowCount = 3;
    private float rotateBowTime=0.2f;
    Transform target;

    protected override void 攻击()
    {
        if (enemyList.Count == 0) return;
        target = 锁定目标();
        if (target == null) return;

        攻击间隔clk = 攻击间隔;

        // 发射
        int sign = ((Vector2)(target.position - transform.position)).IsLeftOf(Vector2.up) ? 1 : -1;
        transform.GetComponent<SpriteRenderer>().flipX = sign < 0;
        animator.speed = Mathf.Max(1.0f, (0.5f + rotateBowTime*2) / (攻击速度 - 0.1f));
        transform.DOLocalRotate(new Vector3(0, 0, sign * 45), rotateBowTime).OnComplete(()=> { animator.SetTrigger("attack"); });

    }

    public void 发射弓箭()
    {
        if (target == null) return;

        Vector3 targetDirection = (target.transform.position - 攻击物生成点.position).normalized;
        float baseAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        for (int i = 0; i < arrowCount; ++i)
        {
            var arrow = GameObject.Instantiate(攻击飞行物, 攻击物生成点.position, Quaternion.identity, ContainerManager.Instance.weaponObjectContainer);
            arrow.GetComponent<武器伤害>().weapon = this;
            arrow.GetComponent<武器伤害>().source = GameManager.Instance.playerGo;

            float angle = baseAngle;
            if (i != arrowCount / 2) // 中间的一只箭不偏移
            {
                int offsetIndex = (i < arrowCount / 2) ? -(arrowCount / 2 - i) : (i - arrowCount / 2);
                angle += offsetIndex * angleOffset;
            }

            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            arrow.GetComponent<FlyingObject>().StartMove(direction);
        }

        transform.DOLocalRotate(new Vector3(0, 0, 0), rotateBowTime);
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

