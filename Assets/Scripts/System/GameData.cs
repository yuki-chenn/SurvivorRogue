using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // 基础设定
    public int weaponSlot = 5;
    public int shopSlot = 4;
    public float saleDiscount = 0.5f;
    public float waveTime = 60;

    // 存档位置
    public int saveIndex = -1;
    public float playTime = 0f;

    // 初始选择的道具和角色
    public GameObject playerPrefab
    {
        get
        {
            return AssetManager.Instance.HeroPrefab[HeroInfo.Index];
        }
    }
    public int selectHeroId = -1;
    public HeroTplInfo HeroInfo
    {
        get
        {
            if (selectHeroId == -1) return null;
            else return TplUtil.GetHeroMap()[selectHeroId];
        }
    } 
    public int initialWeaponId = -1;
    public int initialItemId = -1;

    // 游戏数据
    public int curWave = 0;
    
    // 角色数据
    public PlayerAttribute playerAttr;
    public int salary;
    public BuffList playerBuffs = new BuffList();


    public int level = 1;
    public int curExp = 0;
    public int nextLevelExp = 1000;

    
    public int[] selectionsId;
    public int[] selectionsType;
    // 97-魔术牌
    public bool[] isFree;
    // 下次是否免费刷新
    public bool isNextFree = false;
    public int _money = 0;
    public int money
    {
        get
        {
            return _money;
        }
        set
        {
            OnMoneyChange(_money, value);
            _money = value;
        }
    }
    public int costX = 0;
    public int refreshCount = 0;
    
    // 投资回报记录消耗的金币
    public int 投资回报cost;


    public int[] weaponIDs;
    public Dictionary<int,int> itemIDs;

    public float pickDis = 1.0f;

    // 记录当前波次拾取的宝箱个数
    public int[] chestCount = new int[3];


    // 46-财运背包 需要记录的金币数量
    public int collectMoney = 0;

    // 70-生命的旅程 记录走了多少
    public float totalDistance = 0;
    public float 生命的旅程distance = 0;

    // 80-惬意烟斗计数
    public int 惬意烟斗count = 0;



    public GameData()
    {
        playerAttr = new PlayerAttribute();
        weaponIDs = new int[weaponSlot];
        for (int i = 0; i < weaponSlot; i++)
        {
            weaponIDs[i] = -1;
        }
        itemIDs = new Dictionary<int, int>();
        selectionsId = new int[shopSlot];
        selectionsType = new int[shopSlot];
        isFree = new bool[shopSlot];
        for (int i = 0; i < shopSlot; ++i)
        {
            selectionsId[i] = selectionsType[i] = -1;
            isFree[i] = false;
        }
    }

    public void SetInitialPlayerAttr()
    {
        if(-1 == selectHeroId)
        {
            Debug.LogError("玩家属性初始化错误，请先为selectID赋值");
            return;
        }
        var heroInfo = TplUtil.GetHeroMap()[selectHeroId];
        playerAttr.最大生命 = heroInfo.MaxHp;
        playerAttr.攻击速度 = heroInfo.AttackSpeed;
        playerAttr.力量 = heroInfo.Strength;
        playerAttr.智力 = heroInfo.Intelligence;
        playerAttr.防御 = heroInfo.Defense;
        playerAttr.闪避 = heroInfo.Dodge;
        playerAttr.移动速度 = heroInfo.MoveSpeed;
        playerAttr.暴击率 = heroInfo.Critical;
        playerAttr.暴击伤害 = heroInfo.CriticalDamage;
        playerAttr.幸运 = heroInfo.Luck;
        GameManager.Instance.gameData.salary = heroInfo.Salary;
    }

    public void LevelUpAddAttr()
    {
        if (-1 == selectHeroId)
        {
            Debug.LogError("玩家升级属性增加错误，请先为selectID赋值");
            return;
        }
        var heroInfo = TplUtil.GetHeroMap()[selectHeroId];
        playerAttr.最大生命 += heroInfo.IncMaxHp;
        playerAttr.攻击速度 += heroInfo.IncAttackSpeed;
        playerAttr.力量 += heroInfo.IncStrength;
        playerAttr.智力 += heroInfo.IncIntelligence;
        playerAttr.防御 += heroInfo.IncDefense;
        playerAttr.闪避 += heroInfo.IncDodge;
        playerAttr.移动速度 += heroInfo.IncMoveSpeed;
        playerAttr.暴击率 += heroInfo.IncCritical;
        playerAttr.暴击伤害 += heroInfo.IncCriticalDamage;
        playerAttr.幸运 += heroInfo.IncLuck;
    }

    public void ClearChestCount()
    {
        chestCount[0] = chestCount[1] = chestCount[2] = 0;
    }

    private void OnMoneyChange(int oldValue,int newValue)
    {
        #region 95-投资回报
        // 每消耗100金币，收入增加2
        if (GameManager.Instance.HasItem(ItemEnum.投资回报))
        {
            if(newValue < oldValue)
            {
                int cost = oldValue - newValue;
                投资回报cost += cost;
                while (投资回报cost >= 100)
                {
                    // 增加收入
                    int t = 投资回报cost / 100;
                    salary += GameManager.Instance.HasItemNum(ItemEnum.投资回报) * 2 * t;

                    投资回报cost -= 100 * t;
                }
            }
            
        }
        #endregion
    }


}
