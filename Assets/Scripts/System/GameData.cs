using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    // �����趨
    public int weaponSlot = 5;
    public int shopSlot = 4;
    public float saleDiscount = 0.5f;
    public float waveTime = 60;

    // �浵λ��
    public int saveIndex = -1;
    public float playTime = 0f;

    // ��ʼѡ��ĵ��ߺͽ�ɫ
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

    // ��Ϸ����
    public int curWave = 0;
    
    // ��ɫ����
    public PlayerAttribute playerAttr;
    public int salary;
    public BuffList playerBuffs = new BuffList();


    public int level = 1;
    public int curExp = 0;
    public int nextLevelExp = 1000;

    
    public int[] selectionsId;
    public int[] selectionsType;
    // 97-ħ����
    public bool[] isFree;
    // �´��Ƿ����ˢ��
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
    
    // Ͷ�ʻر���¼���ĵĽ��
    public int Ͷ�ʻر�cost;


    public int[] weaponIDs;
    public Dictionary<int,int> itemIDs;

    public float pickDis = 1.0f;

    // ��¼��ǰ����ʰȡ�ı������
    public int[] chestCount = new int[3];


    // 46-���˱��� ��Ҫ��¼�Ľ������
    public int collectMoney = 0;

    // 70-�������ó� ��¼���˶���
    public float totalDistance = 0;
    public float �������ó�distance = 0;

    // 80-����̶�����
    public int ����̶�count = 0;



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
            Debug.LogError("������Գ�ʼ����������ΪselectID��ֵ");
            return;
        }
        var heroInfo = TplUtil.GetHeroMap()[selectHeroId];
        playerAttr.������� = heroInfo.MaxHp;
        playerAttr.�����ٶ� = heroInfo.AttackSpeed;
        playerAttr.���� = heroInfo.Strength;
        playerAttr.���� = heroInfo.Intelligence;
        playerAttr.���� = heroInfo.Defense;
        playerAttr.���� = heroInfo.Dodge;
        playerAttr.�ƶ��ٶ� = heroInfo.MoveSpeed;
        playerAttr.������ = heroInfo.Critical;
        playerAttr.�����˺� = heroInfo.CriticalDamage;
        playerAttr.���� = heroInfo.Luck;
        GameManager.Instance.gameData.salary = heroInfo.Salary;
    }

    public void LevelUpAddAttr()
    {
        if (-1 == selectHeroId)
        {
            Debug.LogError("��������������Ӵ�������ΪselectID��ֵ");
            return;
        }
        var heroInfo = TplUtil.GetHeroMap()[selectHeroId];
        playerAttr.������� += heroInfo.IncMaxHp;
        playerAttr.�����ٶ� += heroInfo.IncAttackSpeed;
        playerAttr.���� += heroInfo.IncStrength;
        playerAttr.���� += heroInfo.IncIntelligence;
        playerAttr.���� += heroInfo.IncDefense;
        playerAttr.���� += heroInfo.IncDodge;
        playerAttr.�ƶ��ٶ� += heroInfo.IncMoveSpeed;
        playerAttr.������ += heroInfo.IncCritical;
        playerAttr.�����˺� += heroInfo.IncCriticalDamage;
        playerAttr.���� += heroInfo.IncLuck;
    }

    public void ClearChestCount()
    {
        chestCount[0] = chestCount[1] = chestCount[2] = 0;
    }

    private void OnMoneyChange(int oldValue,int newValue)
    {
        #region 95-Ͷ�ʻر�
        // ÿ����100��ң���������2
        if (GameManager.Instance.HasItem(ItemEnum.Ͷ�ʻر�))
        {
            if(newValue < oldValue)
            {
                int cost = oldValue - newValue;
                Ͷ�ʻر�cost += cost;
                while (Ͷ�ʻر�cost >= 100)
                {
                    // ��������
                    int t = Ͷ�ʻر�cost / 100;
                    salary += GameManager.Instance.HasItemNum(ItemEnum.Ͷ�ʻر�) * 2 * t;

                    Ͷ�ʻر�cost -= 100 * t;
                }
            }
            
        }
        #endregion
    }


}
