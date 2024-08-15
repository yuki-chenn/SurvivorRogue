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


    public float ��ս��ȴʱ��;
    public float Զ����ȴʱ��;
    protected float ��սclk;
    protected float Զ��clk;
    public bool ��ս������ȴ��� { get { return ��սclk <= 0; } }
    public bool Զ�̹�����ȴ��� { get { return Զ��clk <= 0; } }

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

        // ���ݵ�ǰ�Ĳ��� ��ߵ��˵���ֵ
        float upRate = (float)Math.Exp(0.1 * (GameManager.Instance.gameData.curWave - 1));
        float speedupRate = (float)Math.Exp(0.01 * (GameManager.Instance.gameData.curWave - 1));
        attr.������ = Info.Attack * upRate;
        attr.�ƶ��ٶ� = Info.MoveSpeed * speedupRate;
        attr.������� = Info.MaxHp * upRate;
        attr.���� = Info.Defense * upRate;
        maxHp = attr.�������;
        curHp = maxHp;

        #region 68-��ľ���
        // ���е��˼���1%���ƶ��ٶ�
        if (GameManager.Instance.HasItem(ItemEnum.��ľ���))
        {
            attr.�ƶ��ٶ� *= (1 - 0.01f * GameManager.Instance.HasItemNum(ItemEnum.��ľ���));
        }
        #endregion

    }

    public virtual void ��ս����() { }

    public virtual void Զ�̹���() { }

    public virtual void �ƶ�() { }

    public virtual void ����() { }

    public override void Die(float time)
    {
        int ˫���漣���� = 1;
        #region 47-˫���漣
        if (GameManager.Instance.HasItem(ItemEnum.˫���漣) && Info.IsBoss == 0)
        {
            if(RandomUtil.IsProbabilityMet(GameManager.Instance.Player.attr.������ / 100f))
            {
                ˫���漣���� = 2;
            }
        }
        #endregion

        
        DropAward(˫���漣����);
        GainExperience(˫���漣����);
        
        base.Die(time);
    }

    public virtual void DropAward(int moneyTimes=1)
    {
        if (Info == null || Info.DropId == -1) return;
        var drop = TplUtil.GetDropMap()[Info.DropId];

        // ������
        var dropMoney = drop.Money;
        // ���˲��� 
        float luck = GameManager.Instance.Player.attr.����;
        // 2����luck / 2000
        if (RandomUtil.IsProbabilityMet(luck / 2000f))
        {
            dropMoney *= 2;
        }
        // 1.5����luck / 1000
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

        // Ѫƿ
        float HealthPotionRate = drop.HealthPotionRate + (luck / 2000);

        #region 63-�ɺ��ո���
        // �������Ѫƿ�ĸ������100%
        if (GameManager.Instance.HasItem(ItemEnum.�ɺ��ո���))
        {
            HealthPotionRate += HealthPotionRate * GameManager.Instance.HasItemNum(ItemEnum.�ɺ��ո���);
        }
        #endregion
        //Debug.LogError("potion rate:" + HealthPotionRate);
        if (RandomUtil.IsProbabilityMet(HealthPotionRate))
        {
            GameManager.Instance.GenerateDrop(DropEnum.HealthPotion, transform.position);
        }

        // ����
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

        #region 52-�ľ� 90-��������
        // ���ܹ����õľ�������20 %��
        if (GameManager.Instance.HasItem(ItemEnum.�ľ�))
        {
            gainExp += (int)(Info.GainExp * 0.2f * GameManager.Instance.HasItemNum(ItemEnum.�ľ�));
        }
        // ÿӵ��1%�ı����ʣ����ܹ����õľ�������1%
        if (GameManager.Instance.HasItem(ItemEnum.��������))
        {
            gainExp += (int)(Info.GainExp * 0.01f * (int)GameManager.Instance.Player.attr.������ * GameManager.Instance.HasItemNum(ItemEnum.��������));
        }
        #endregion

        GameManager.Instance.GainExp(gainExp * expTimes);
    }


}
