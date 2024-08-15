using Survivor.Template;
using Survivor.Utils;
using System;
using UnityEngine;




[Serializable]
public class BaseBuff
{
    public virtual int ID { get; set; }

    public BuffTplInfo Info { get { return TplUtil.GetBuffMap()[ID]; } }

    public BuffAddType AddType { get { return (BuffAddType)Info.AddType; } }
    public BuffRemoveType RemoveType { get { return (BuffRemoveType)Info.RemoveType; } }

    [NonSerialized]
    public GameObject creater;
    [NonSerialized]
    public GameObject owner;

    public int curStack;

    public int tickCount = 0;

    // 计时器
    public float clkDuration;

    public float clkTick = 0;

    // 手动设置需要删除的buff
    public bool needRemove = false;

    public BaseBuff()
    {
        curStack = 1;
        tickCount = 0;
        clkDuration = Info.Duration;
        clkTick = Info.TickTime;
    }

    protected virtual void AddStack()
    {
        if (Info.MaxStack != -1 && curStack >= Info.MaxStack) return;
        curStack += 1;
    }

    // 当增加buff的type为modify时调用
    public virtual void AddModify()
    {
        if (AddType == BuffAddType.Modify)
        {
            Debug.LogError(Info.Name + "未重写AddModify方法");
        }
    }

    public virtual void ReduceStack()
    {
        curStack -= 1;
    }






    /// <summary>
    /// 当wave开始时触发
    /// </summary>
    public virtual void OnWaveStart()
    {
        Debug.Log(Info.Name + " OnWaveStart");
    }

    /// <summary>
    /// 当wave结束时触发
    /// </summary>
    public virtual void OnWaveEnd()
    {
        Debug.Log(Info.Name + " OnWaveEnd");
    }


    /// <summary>
    /// wave时每帧调用
    /// </summary>
    public virtual void OnUpdate()
    {
        //Debug.Log(Info.Name + " OnUpdate");
        if (Info.Duration != -1)
        {
            clkDuration -= Time.deltaTime;
        }

        if(Info.TickTime != -1)
        {
            clkTick -= Time.deltaTime;
            if(clkTick <= 0 && (Info.MaxTickCount == -1 || tickCount < Info.MaxTickCount))
            {
                OnTick();
                clkTick = Info.TickTime;
            }
        }

    }



    /// <summary>
    /// 当Buff被添加时触发
    /// </summary>
    public virtual void OnAdd()
    {
        Debug.Log(Info.Name + " OnAdd");
    }

    // 当Buff被移除时触发
    public virtual void OnRemove()
    {
        Debug.Log(Info.Name + " OnRemove");
    }

    /// <summary>
    /// 产生DamageInfo之后，调用takeDamage之前触发（被攻击方）
    /// 主要是一些伤害减免的buff
    /// </summary>
    public virtual void OnBeforeTakeDamage(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnBeforeTakeDamage");
    }

    /// <summary>
    /// 产生DamageInfo之后，调用takeDamage之前触发（攻击方）
    /// 主要是一些伤害增加的buff
    /// </summary>
    public virtual void OnBeforeGiveDamage(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnBeforeGiveDamage");
    }

    /// <summary>
    /// 当角色对其他角色造成伤害时触发
    /// </summary>
    public virtual void OnHurt(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnHurt");
    }

    /// <summary>
    /// 当角色受到伤害时触发
    /// </summary>
    public virtual void OnHurted(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnHurted");
    }

    public virtual void OnDodge()
    {
        Debug.Log(Info.Name + " OnDodge");
    }

    public virtual void OnCritical()
    {
        Debug.Log(Info.Name + " OnCritical");
    }

    /// <summary>
    /// 当将要收到致命伤害之前触发
    /// 主要是一些抵挡致死伤害的buff
    /// </summary>
    public virtual void OnBeforeKilled(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnBeforeKilled");
    }

    /// <summary>
    /// 击杀其他角色时触发
    /// 击杀加成等buff
    /// </summary>
    public virtual void OnKill()
    {
        Debug.Log(Info.Name + " OnKill");
    }

    /// <summary>
    /// 被其他角色击杀时触发
    /// 击杀加成等buff
    /// </summary>
    public virtual void OnKilled()
    {
        Debug.Log(Info.Name + " OnKilled");
    }

    /// <summary>
    /// 角色间隔Tick触发
    /// dot伤害之类的
    /// </summary>
    public virtual void OnTick()
    {
        Debug.Log(Info.Name + " OnTick");
        tickCount++;
    }


}
