using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{

    // 动画控制器
    protected Animator animator;

    // 跟随人物的UI
    protected Transform UItrans;

    // 刚体
    protected Rigidbody2D rigidBody;

    public float maxHp;
    public float _hp;
    public float curHp
    {
        get
        {
            return _hp;
        }
        set
        {
            if(_hp != value)
            {
                var oldValue = _hp;
                _hp = value;
                OnHPChanged(oldValue, value);
            }
        }
    }
    public bool isDead = false;

    public BuffList buffList = new BuffList();

    private Slider bloodBar;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        InitAnimation();
        UItrans = transform.Find("UI");
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        bloodBar = transform.Find("UI").GetComponentInChildren<Slider>();
    }

    protected virtual void Update()
    {
        // 调用每个的update
        foreach (var buff in buffList.buffs)
        {
            buff.OnUpdate();
        }

        // 看有没有需要去除的buff
        var buffsToRemove = new List<BaseBuff>();

        foreach (var buff in buffList.buffs)
        {
            if ((buff.Info.MaxTickCount != -1 && buff.tickCount == buff.Info.MaxTickCount) ||
                (buff.Info.Duration != -1 && buff.clkDuration <= 0) || 
                (buff.needRemove))
            {
                buff.OnRemove();
                buffsToRemove.Add(buff);
            }
        }

        foreach (var buff in buffsToRemove)
        {
            buffList.buffs.Remove(buff);
        }

    }

    // 恢复所有血量
    public void RestoreAllHp()
    {
        curHp = maxHp;
    }

    protected virtual void OnHPChanged(float oldHp,float newHp)
    {
        // 更新血条
        UpdateBloodBar();
        //Debug.LogError(string.Format(gameObject.name + " hp change : {0} -> {1}", oldHp, newHp));
    }


    public virtual void TakeDamage(DamageInfo damage)
    {
        // 伤害显示
        DamagePopupManager.Instance.CreateDamageHint(transform.position, damage);

        if (damage.Value < 0) return ;
        float cur = curHp;
        curHp = Math.Max(0, curHp - damage.Value);

        
    }

    public void UpdateBloodBar()
    {
        if(bloodBar != null) bloodBar.value = _hp / maxHp;
    }

    public virtual void Die(float time)
    {
        if (curHp > 0) return;
        isDead = true;
        animator.SetBool("isDead", true);
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        GameObjectUtil.SetAllChildGameObjectEnable(transform,false);
        GameObject.Destroy(gameObject, time);
    }


    public virtual void InitAttribute() { }

    // 动画的初始工作
    private void InitAnimation()
    {
        animator = GetComponent<Animator>();
        if(animator == null)
        {
            Debug.Log(gameObject.name + "没有Animator");
        }
    }

}
