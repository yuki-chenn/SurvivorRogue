using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseCharacter
{

    public int id = -1;
    private EnemyTplInfo Info { get { return id == -1 ? null : TplUtil.GetEnemyMap()[id]; } }
    public EnemyAttribute attr;


    public float 近战冷却时间;
    public float 远程冷却时间;
    protected float 近战clk;
    protected float 远程clk;
    public bool 近战攻击冷却完毕 { get { return 近战clk <= 0; } }
    public bool 远程攻击冷却完毕 { get { return 远程clk <= 0; } }

    protected override void Start()
    {
        base.Start();
        InitAttribute();
    }

    public override void InitAttribute()
    {
        base.InitAttribute();
        if (Info == null) return;
        attr = new EnemyAttribute();

        // 根据当前的波次 提高敌人的数值
        float upRate = (float)Math.Exp(0.1 * (GameManager.Instance.gameData.curWave - 1));
        float speedupRate = (float)Math.Exp(0.01 * (GameManager.Instance.gameData.curWave - 1));
        attr.攻击力 = Info.Attack * upRate;
        attr.移动速度 = Info.MoveSpeed * speedupRate;
        attr.最大生命 = Info.MaxHp * upRate;
        attr.防御 = Info.Defense * upRate;
        maxHp = attr.最大生命;
        curHp = maxHp;

        #region 68-橡木棺椁
        // 所有敌人减缓1%的移动速度
        if (GameManager.Instance.HasItem(ItemEnum.橡木棺椁))
        {
            attr.移动速度 *= (1 - 0.01f * GameManager.Instance.HasItemNum(ItemEnum.橡木棺椁));
        }
        #endregion

    }

    public virtual void 近战攻击() { }

    public virtual void 远程攻击() { }

    public virtual void 移动() { }

    public virtual void 待机() { }

    public override void Die(float time)
    {
        int 双倍奇迹倍率 = 1;
        #region 47-双倍奇迹
        if (GameManager.Instance.HasItem(ItemEnum.双倍奇迹) && Info.IsBoss == 0)
        {
            if(RandomUtil.IsProbabilityMet(GameManager.Instance.Player.attr.暴击率 / 100f))
            {
                双倍奇迹倍率 = 2;
            }
        }
        #endregion

        
        DropAward(双倍奇迹倍率);
        GainExperience(双倍奇迹倍率);
        
        base.Die(time);
    }

    public virtual void DropAward(int moneyTimes=1)
    {
        if (Info == null || Info.DropId == -1) return;
        var drop = TplUtil.GetDropMap()[Info.DropId];

        // 掉落金币
        var dropMoney = drop.Money;
        // 幸运补偿 
        float luck = GameManager.Instance.Player.attr.幸运;
        // 2倍：luck / 2000
        if (RandomUtil.IsProbabilityMet(luck / 2000f))
        {
            dropMoney *= 2;
        }
        // 1.5倍：luck / 1000
        else if (RandomUtil.IsProbabilityMet(luck / 1000f))
        {
            dropMoney += dropMoney / 2;
        }

        dropMoney *= moneyTimes;

        for (int i = 0; i < dropMoney; ++i)
        {
            GameManager.Instance.GenerateDrop(DropEnum.Money, transform.position);
            //Instantiate(AssetManager.Instance.DropPrefab[(int)DropEnum.Money], transform.position, Quaternion.identity, ContainerManager.Instance.dropObjectContainer);
        }
        //Debug.LogError(string.Format("prob:{0},{1},{2},{3}", drop.HealthPotionRate + (luck / 5000), drop.RareChestRate + (luck / 1000),
        //    drop.epicChestRate + (luck / 2000), drop.LegendChestRate + (luck / 4000)));

        // 血瓶
        float HealthPotionRate = drop.HealthPotionRate + (luck / 2000);

        #region 63-猩红收割者
        // 怪物掉落血瓶的概率提高100%
        if (GameManager.Instance.HasItem(ItemEnum.猩红收割者))
        {
            HealthPotionRate += HealthPotionRate * GameManager.Instance.HasItemNum(ItemEnum.猩红收割者);
        }
        #endregion
        //Debug.LogError("potion rate:" + HealthPotionRate);
        if (RandomUtil.IsProbabilityMet(HealthPotionRate))
        {
            GameManager.Instance.GenerateDrop(DropEnum.HealthPotion, transform.position);
        }

        // 宝箱
        if (RandomUtil.IsProbabilityMet(drop.RareChestRate + (luck / 4000)))
        {
            GameManager.Instance.GenerateDrop(DropEnum.RareChest, transform.position);
            //Instantiate(AssetManager.Instance.DropPrefab[(int)DropEnum.RareChest], transform.position, Quaternion.identity, ContainerManager.Instance.dropObjectContainer); ;
        }
        if (RandomUtil.IsProbabilityMet(drop.EpicChestRate + (luck / 6000)))
        {
            GameManager.Instance.GenerateDrop(DropEnum.EpicChest, transform.position);
            //Instantiate(AssetManager.Instance.DropPrefab[(int)DropEnum.EpicChest], transform.position, Quaternion.identity, ContainerManager.Instance.dropObjectContainer); ;
        }
        if (RandomUtil.IsProbabilityMet(drop.LegendChestRate + (luck / 8000)))
        {
            GameManager.Instance.GenerateDrop(DropEnum.LegendChest, transform.position);
            //Instantiate(AssetManager.Instance.DropPrefab[(int)DropEnum.LegendChest], transform.position, Quaternion.identity, ContainerManager.Instance.dropObjectContainer); ;
        }

    }

    public virtual void GainExperience(int expTimes=1)
    {
        int gainExp = Info.GainExp;

        #region 52-文具 90-暴击领悟
        // 击败怪物获得的经验增加20 %。
        if (GameManager.Instance.HasItem(ItemEnum.文具))
        {
            gainExp += (int)(Info.GainExp * 0.2f * GameManager.Instance.HasItemNum(ItemEnum.文具));
        }
        // 每拥有1%的暴击率，击败怪物获得的经验增加1%
        if (GameManager.Instance.HasItem(ItemEnum.暴击领悟))
        {
            gainExp += (int)(Info.GainExp * 0.01f * (int)GameManager.Instance.Player.attr.暴击率 * GameManager.Instance.HasItemNum(ItemEnum.暴击领悟));
        }
        #endregion

        GameManager.Instance.GainExp(gainExp * expTimes);
    }


}
