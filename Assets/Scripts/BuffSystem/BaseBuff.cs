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

    // ��ʱ��
    public float clkDuration;

    public float clkTick = 0;

    // �ֶ�������Ҫɾ����buff
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

    // ������buff��typeΪmodifyʱ����
    public virtual void AddModify()
    {
        if (AddType == BuffAddType.Modify)
        {
            Debug.LogError(Info.Name + "δ��дAddModify����");
        }
    }

    public virtual void ReduceStack()
    {
        curStack -= 1;
    }






    /// <summary>
    /// ��wave��ʼʱ����
    /// </summary>
    public virtual void OnWaveStart()
    {
        Debug.Log(Info.Name + " OnWaveStart");
    }

    /// <summary>
    /// ��wave����ʱ����
    /// </summary>
    public virtual void OnWaveEnd()
    {
        Debug.Log(Info.Name + " OnWaveEnd");
    }


    /// <summary>
    /// waveʱÿ֡����
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
    /// ��Buff�����ʱ����
    /// </summary>
    public virtual void OnAdd()
    {
        Debug.Log(Info.Name + " OnAdd");
    }

    // ��Buff���Ƴ�ʱ����
    public virtual void OnRemove()
    {
        Debug.Log(Info.Name + " OnRemove");
    }

    /// <summary>
    /// ����DamageInfo֮�󣬵���takeDamage֮ǰ����������������
    /// ��Ҫ��һЩ�˺������buff
    /// </summary>
    public virtual void OnBeforeTakeDamage(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnBeforeTakeDamage");
    }

    /// <summary>
    /// ����DamageInfo֮�󣬵���takeDamage֮ǰ��������������
    /// ��Ҫ��һЩ�˺����ӵ�buff
    /// </summary>
    public virtual void OnBeforeGiveDamage(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnBeforeGiveDamage");
    }

    /// <summary>
    /// ����ɫ��������ɫ����˺�ʱ����
    /// </summary>
    public virtual void OnHurt(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnHurt");
    }

    /// <summary>
    /// ����ɫ�ܵ��˺�ʱ����
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
    /// ����Ҫ�յ������˺�֮ǰ����
    /// ��Ҫ��һЩ�ֵ������˺���buff
    /// </summary>
    public virtual void OnBeforeKilled(DamageInfo info)
    {
        Debug.Log(Info.Name + " OnBeforeKilled");
    }

    /// <summary>
    /// ��ɱ������ɫʱ����
    /// ��ɱ�ӳɵ�buff
    /// </summary>
    public virtual void OnKill()
    {
        Debug.Log(Info.Name + " OnKill");
    }

    /// <summary>
    /// ��������ɫ��ɱʱ����
    /// ��ɱ�ӳɵ�buff
    /// </summary>
    public virtual void OnKilled()
    {
        Debug.Log(Info.Name + " OnKilled");
    }

    /// <summary>
    /// ��ɫ���Tick����
    /// dot�˺�֮���
    /// </summary>
    public virtual void OnTick()
    {
        Debug.Log(Info.Name + " OnTick");
        tickCount++;
    }


}
