using Survivor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DamageInfo
{
    public GameObject source;
    public GameObject target;

    // 伤害的基础数值
    public float amount;

    // 闪避的概率
    public float dodgeRate;

    // 防御减免
    public float defense;

    // 伤害减免
    public float reduction;

    // 伤害的暴击率
    public float criticalRate;

    // 伤害的暴击伤害
    public float criticalDamage;

    // 伤害加成
    public float bonus = 100.0f;

    // 标记伤害类型
    private List<string> tags;

    // 伤害暴击闪避结果
    public bool isCritical = false;
    public bool isMiss = false;

    // 最终造成的伤害
    public float Value
    {
        get
        {
            if (isMiss) return 0.0f;
            // 防御减免
            //var dmg = Mathf.Max(0, amount - defense);
            var val = amount * amount / (amount + defense);

            // 伤害减免
            val *= (1 - Mathf.Min(1.0f, reduction / 100.0f));

            // 暴击
            if (isCritical)
            {
                val *= criticalDamage / 100.0f;
            }
            val *= (bonus / 100.0f);

            return val;
        }
    }


    public DamageInfo() { }

    public DamageInfo(GameObject source, GameObject target, float amount) : this()
    {
        this.source = source;
        this.target = target;
        this.amount = amount;
    }

    public DamageInfo(
        GameObject source, GameObject target,float amount,
        float defense, float dodgeRate,
        float criticalRate, float criticalDamage=100.0f)
        :this(source, target, amount)
    {
        this.dodgeRate = dodgeRate;
        this.defense = defense;
        this.criticalRate = criticalRate;
        this.criticalDamage = criticalDamage;
        SetIsMissAndCritical();
    }

    private void SetIsMissAndCritical()
    {
        isMiss = RandomUtil.IsProbabilityMet(dodgeRate / 100.0f);
        isCritical = RandomUtil.IsProbabilityMet(criticalRate / 100.0f);
    }

}

