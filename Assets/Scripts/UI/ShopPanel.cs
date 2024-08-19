using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Survivor.Utils;
using Survivor.Template;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopPanel : MonoBehaviour
{
    private GameData gameData { get { return GameManager.Instance.gameData; } }

    private CanvasGroup canvasGroup;

    private Button btnContinue;
    private Button btnRefreshSelections;
    private Button btnBackMenu;

    private Transform weapons;
    private Transform selections;

    private List<Text> txtAttr = new List<Text>();

    private TextMeshProUGUI txtRefreshCost;

    public bool[] isLock;

    private int _refreshCost = 10;
    private int refreshCost
    {
        get
        {
            return (int)(_refreshCost * RefreshDiscount);
        }
        set
        {
            _refreshCost = value;
        }
    }

    private string MONEY_TXT_PREFIX = "<sprite=0> ";

    

    private float WeaponDiscount
    {
        get
        {
            float discount = 1.0f;
            if (GameManager.Instance.HasItem(ItemEnum.普通会员)) discount -= 0.05f;
            if (GameManager.Instance.HasItem(ItemEnum.高级会员)) discount -= 0.05f;
            if (GameManager.Instance.HasItem(ItemEnum.VIP会员)) discount -= 0.05f;
            return discount;
        }
    }

    private float ItemDiscount
    {
        get
        {
            float discount = 1.0f;
            if (GameManager.Instance.HasItem(ItemEnum.高级会员)) discount -= 0.05f;
            if (GameManager.Instance.HasItem(ItemEnum.VIP会员)) discount -= 0.1f;
            return discount;
        }
    }

    private float RefreshDiscount
    {
        get
        {
            float discount = 1.0f;
            if (GameManager.Instance.HasItem(ItemEnum.VIP会员)) discount -= 0.1f;
            return discount;
        }
    }





    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowShopPanel, Show);
        EventCenter.AddListener(EventDefine.HideShopPanel, Hide);
        EventCenter.AddListener(EventDefine.OnWeaponSaled, OnWeaponSaled);
        EventCenter.AddListener(EventDefine.OnWeaponRanked, OnWeaponRanked);
        EventCenter.AddListener(EventDefine.OnChestItemSelect, OnChestItemSelect);

        canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        btnContinue = transform.Find("ContinueBtn").GetComponent<Button>();
        btnContinue.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            EventCenter.Broadcast(EventDefine.HideShopPanel);
        });

        btnRefreshSelections = transform.Find("RefreshBtn").GetComponent<Button>();
        btnRefreshSelections.onClick.AddListener(() =>
        {
            
            if (gameData.isNextFree)
            {
                gameData.isNextFree = false;
            }
            else
            {
                if (gameData.money < refreshCost)
                {
                    // 展示钱不够
                    GameUIManager.Instance.LackOfMoney();
                    return;
                }
                gameData.money -= refreshCost;
                gameData.costX++;
                refreshCost = (int)(10 * Mathf.Exp(0.15f * gameData.costX));
            }
            AudioManager.Instance.PlayRefreshSelectionsEffect();
            gameData.refreshCount++;



            #region 持有道具 44-筹码50 80-惬意烟斗
            // 每次刷新商店时，有50 % 的概率使下次刷新免费。
            if (GameManager.Instance.HasItem(ItemEnum.筹码50))
            {

                if (RandomUtil.IsProbabilityMet(0.5f))
                {
                    gameData.isNextFree = true;
                }
            }

            // 每刷新5次商店，最大生命增加2
            if (GameManager.Instance.HasItem(ItemEnum.惬意烟斗))
            {
                gameData.惬意烟斗count += 1;
                if (gameData.惬意烟斗count > 0 && gameData.惬意烟斗count % 5 == 0)
                {
                    GameManager.Instance.gameData.playerAttr.最大生命 += 2 * GameManager.Instance.HasItemNum(ItemEnum.惬意烟斗);
                    RefreshPlayerAttr();
                }
            }

            #endregion

            ReRollSelections();
            RefreshAllShopSelections();
            RefreshMoney();
            RefreshPlayerAttr();

        });

        btnBackMenu = transform.Find("BackMenuBtn").GetComponent<Button>();
        btnBackMenu.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            // 存档
            GameManager.Instance.SaveGameData(GameManager.Instance.gameData.saveIndex);
            GameManager.Instance.OverridePlayerPrefs(GameManager.Instance.gameData.saveIndex);

            GameManager.Instance.fsm.PerformTransition(Transition.BackMenu);
            SceneManager.LoadScene("MenuScene");
        });

        weapons = transform.Find("Weapons");
        selections = transform.Find("BuySelections");

        Transform attrTrans = transform.Find("PlayerAttr");
        foreach (Transform child in attrTrans)
        {
            txtAttr.Add(child.Find("AttrValue").GetComponent<Text>());
        }
        txtRefreshCost = transform.Find("RefreshBtn/RefreshCost").GetComponent<TextMeshProUGUI>();

        InitShopSelections();

        gameObject.SetActive(false);

    }



    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideShopPanel, Hide);
        EventCenter.RemoveListener(EventDefine.OnWeaponSaled, OnWeaponSaled);
        EventCenter.RemoveListener(EventDefine.OnWeaponRanked, OnWeaponRanked);
        EventCenter.RemoveListener(EventDefine.OnChestItemSelect, OnChestItemSelect);
    }

    void Start()
    {
        
    }

    // 初始化商店道具
    private void InitShopSelections()
    {
        isLock = new bool[gameData.shopSlot];
    }

    #region 刷新界面方法

    // 刷新武器栏
    private void RefreshWeaponList()
    {
        for(int i = 0; i < gameData.weaponSlot; ++i)
        {
            GameObject weaponGo = null;
            if (i < weapons.childCount) weaponGo = weapons.GetChild(i).gameObject;
            else weaponGo = Instantiate(AssetManager.Instance.商店武器显示模板, weapons);

            var btn = weaponGo.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            Sprite weaponIcon = null;
            if (gameData.weaponIDs[i] != -1)
            {
                WeaponTplInfo info = TplUtil.GetWeaponMap()[gameData.weaponIDs[i]];
                weaponIcon = AssetManager.Instance.weaponSprite[info.Index];
                int index = i;
                btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlayButtonCliclkEffect();
                    EventCenter.Broadcast<WeaponTplInfo, Vector2, int>(EventDefine.ShowWeaponPopUpPanel, info, weaponGo.transform.position, index);
                });
                weaponGo.transform.Find("WeaponIcon").GetComponent<Image>().sprite = weaponIcon;
                weaponGo.transform.Find("bg").GetComponent<Image>().color = Constants.RANK_COLOR[info.Rank];
                foreach (Transform trans in weaponGo.transform)
                {
                    trans.gameObject.SetActive(true);
                }
            }
            else
            {
                foreach(Transform trans in weaponGo.transform)
                {
                    trans.gameObject.SetActive(false);
                }
            }
           
        }
    }

    // 刷新人物属性列表
    private void RefreshPlayerAttr()
    {
        txtAttr[0].text = gameData.playerAttr.最大生命.ToString("F0");
        txtAttr[1].text = gameData.playerAttr.攻击速度.ToString("F0");
        txtAttr[2].text = gameData.playerAttr.力量.ToString("F0");
        txtAttr[3].text = gameData.playerAttr.智力.ToString("F0");
        txtAttr[4].text = gameData.playerAttr.防御.ToString("F0");
        txtAttr[5].text = gameData.playerAttr.闪避.ToString("F1") + "%";
        txtAttr[6].text = gameData.playerAttr.暴击率.ToString("F1") + "%";
        txtAttr[7].text = gameData.playerAttr.暴击伤害.ToString("F1") + "%";
        txtAttr[8].text = gameData.playerAttr.移动速度.ToString("F0");
        txtAttr[9].text = gameData.playerAttr.幸运.ToString("F0");
        txtAttr[10].text = gameData.salary.ToString();

    }

    // 刷新商店物品
    private void RefreshAllShopSelections()
    {
        for (int i = 0; i < gameData.shopSlot; ++i)
        {
            GameObject selGo = null;
            if (i < selections.childCount) selGo = selections.GetChild(i).gameObject;
            else selGo = Instantiate(AssetManager.Instance.武器道具购买模板, selections);

            RefreshSelections(selGo, i);


        }
    }

    // 商店单个物品的刷新
    private void RefreshSelections(GameObject go,int index)
    {
        if(gameData.selectionsType[index] == -1)
        {
            GameObjectUtil.SetAllChildGameObjectEnable(go.transform, false);
            go.GetComponent<Image>().enabled = false;
            return;
        }
        else
        {
            GameObjectUtil.SetAllChildGameObjectEnable(go.transform, true);
            go.GetComponent<Image>().enabled = true;
        }

        Image bg = go.GetComponent<Image>();
        Image border = go.transform.Find("Border").GetComponent<Image>();

        Image imgIcon = go.transform.Find("Item/Icon/ItemIcon").GetComponent<Image>();
        Image imgIconBorder = go.transform.Find("Item/Icon").GetComponent<Image>();
        Image imgIconBg = go.transform.Find("Item/Icon/Bg").GetComponent<Image>();
        Text txtName = go.transform.Find("Item/Name").GetComponent<Text>();
        Text txtRank = go.transform.Find("Item/Rank").GetComponent<Text>();
        TextMeshProUGUI txtPrice = go.transform.Find("Item/Price").GetComponent<TextMeshProUGUI>();
        Text txtDes = go.transform.Find("Item/DescriptionScroll/Viewport/Text").GetComponent<Text>();
        GameObject weaponFlag = go.transform.Find("Item/WeaponFlag").gameObject;
        Image imgIsLock = go.transform.Find("Lock").GetComponent<Image>();
        Button btnBuy = go.transform.Find("Item").GetComponent<Button>();
        Button btnLock = go.transform.Find("Lock").GetComponent<Button>();

        if (gameData.selectionsType[index] == 0)
        {
            // 武器
            WeaponTplInfo info = TplUtil.GetWeaponMap()[gameData.selectionsId[index]];
            bg.color = Constants.RANK_COLOR_BG[info.Rank];
            border.sprite = AssetManager.Instance.商店武器道具购买等级边框[info.Rank - 1];
            imgIconBg.sprite = AssetManager.Instance.武器道具背景[info.Rank - 1];
            imgIconBorder.sprite = AssetManager.Instance.武器道具等级边框[info.Rank - 1];
            imgIcon.sprite = AssetManager.Instance.weaponSprite[info.Index];
            txtName.text = info.Name;
            txtRank.text = Constants.RANK_NAME[info.Rank];
            txtRank.color = Constants.RANK_COLOR[info.Rank];
            txtPrice.SetText(MONEY_TXT_PREFIX + (gameData.isFree[index] ? "FREE" : ((int)(info.Price * WeaponDiscount)).ToString()));
            txtDes.text = info.Description;
            weaponFlag.SetActive(true);
            go.name = string.Format("weapon-{0}-{1}",info.ID,info.Name);
        }
        else
        {
            // 道具
            ItemTplInfo info = TplUtil.GetItemMap()[gameData.selectionsId[index]];
            bg.color = Constants.RANK_COLOR_BG[info.Rank];
            border.sprite = AssetManager.Instance.商店武器道具购买等级边框[info.Rank - 1];
            imgIconBg.sprite = AssetManager.Instance.武器道具背景[info.Rank - 1];
            imgIconBorder.sprite = AssetManager.Instance.武器道具等级边框[info.Rank - 1];
            imgIcon.sprite = AssetManager.Instance.itemSprite[info.Index];
            txtName.text = info.Name;
            txtRank.text = Constants.RANK_NAME[info.Rank];
            txtRank.color = Constants.RANK_COLOR[info.Rank];
            txtPrice.SetText(MONEY_TXT_PREFIX + (gameData.isFree[index] ? "FREE" : ((int)(info.Price * ItemDiscount)).ToString()));
            txtDes.text = info.Description;
            weaponFlag.SetActive(false);
            go.name = string.Format("item-{0}-{1}", info.ID, info.Name);
        }

        RefreshLockState(index);

        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() =>
        {
            BuySelections(index);
        });

        btnLock.onClick.RemoveAllListeners();
        btnLock.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayLockSelectionsEffect();
            isLock[index] = !isLock[index];
            RefreshLockState(index);
        });

    }

    // 刷新上锁情况
    private void RefreshLockState(int index)
    {
        selections.GetChild(index).Find("Lock").GetComponent<Image>().sprite = isLock[index] ?
            AssetManager.Instance.shopItemLockSprite :
            AssetManager.Instance.shopItemUnLockSprite;
    }

    // 刷新金币
    private void RefreshMoney()
    {
        GameUIManager.Instance.UpdateMoney(gameData.money);
        txtRefreshCost.SetText(MONEY_TXT_PREFIX + string.Format("{0}", gameData.isNextFree ? "FREE" : refreshCost.ToString()));
    }

    private void RefreshPanel()
    {
        RefreshWeaponList();
        RefreshPlayerAttr();
        RefreshAllShopSelections();
        RefreshMoney();
    }

    #endregion

    private void ReRollSelections()
    {
        GameManager.Instance.RefreshShopSelections(isLock, ref gameData.selectionsType, ref gameData.selectionsId, ref gameData.isFree);
    }

    private void BuySelections(int index)
    {
        if (gameData.selectionsType[index] == 0)
        {
            int buyAndRankup = -1;
            WeaponTplInfo info = TplUtil.GetWeaponMap()[gameData.selectionsId[index]];

            int price = gameData.isFree[index] ? 0 : (int)(info.Price * WeaponDiscount);

            if (gameData.money < price)
            {
                // 钱不够
                GameUIManager.Instance.LackOfMoney();
                return;
            }
            
            if (GameManager.Instance.CanBuyWeapon() == -1)
            {
                // 先看看这把武器能不能合成
                if (info.RankupWeaponID == -1) return;
                // 满了，看看能不能和背包中的合成
                buyAndRankup = HasWeapon(info);
                if(buyAndRankup == -1) return;
            }
            if(buyAndRankup != -1) GameManager.Instance.BuyWeaponAndRankUp(info, buyAndRankup, WeaponDiscount, gameData.isFree[index]);
            else GameManager.Instance.BuyWeapon(info, WeaponDiscount, gameData.isFree[index]);
        }
        else
        {
            ItemTplInfo info = TplUtil.GetItemMap()[gameData.selectionsId[index]];

            int price = gameData.isFree[index] ? 0 : (int)(info.Price * ItemDiscount);

            if (gameData.money < price)
            {
                // 展示钱不够
                GameUIManager.Instance.LackOfMoney();
                return;
            }
            GameManager.Instance.BuyItem(info, ItemDiscount, gameData.isFree[index]);
        }


        isLock[index] = false;
        gameData.selectionsType[index] = gameData.selectionsId[index] = -1;
        gameData.isFree[index] = false;

        CheckBuyAll();
        RefreshPanel();
    }

    private int HasWeapon(WeaponTplInfo info)
    {
        for (int i = 0; i < gameData.weaponSlot; ++i)
        {
            if (info.ID == gameData.weaponIDs[i])
            {
                return i ;
            }
        }
        return -1;
    }

    private void CheckBuyAll()
    {
        for(int i = 0; i < gameData.shopSlot; ++i)
        {
            if (gameData.selectionsId[i] != -1) return;
        }
        ReRollSelections();
    }

    private void OnWeaponSaled()
    {
        RefreshMoney();
        RefreshWeaponList();
        RefreshPlayerAttr();
    }

    private void OnWeaponRanked()
    {
        RefreshWeaponList();
        RefreshPlayerAttr();
        RefreshMoney();
    }

    private void OnChestItemSelect()
    {
        RefreshPanel();
    }

    private void Hide()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => 
        {
            gameObject.SetActive(false);
            GameManager.Instance.fsm.PerformTransition(Transition.BuyEnd);
        });
    }

    private void Show()
    {
        refreshCost = (int)(10 * Mathf.Exp(0.15f * gameData.costX));
        gameObject.SetActive(true);
        InitShopSelections();
        RefreshPanel();
        canvasGroup.DOFade(1, 0.5f);
    }

    string itemId;
    private void OnGUI()
    {
#if UNITY_EDITOR
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 50; // Set font size

        GUIStyle textFieldStyle = new GUIStyle(GUI.skin.textField);
        textFieldStyle.fontSize = 50; // Set font size for the input field

        // Create an input field to enter the item ID
        itemId = GUI.TextField(new Rect(500, 200, 200, 60), itemId, textFieldStyle);

        // Create a button, if clicked, return true
        if (GUI.Button(new Rect(500, 300, 200, 100), "获取道具", buttonStyle))
        {
            if (int.TryParse(itemId, out int id))
            {
                ItemTplInfo info = TplUtil.GetItemMap()[id];
                GameManager.Instance.BuyItem(info,0,true);
            }
            else
            {
                Debug.LogError("请输入有效的道具ID");
            }
            RefreshPanel();
        }
        // Create a button, if clicked, return true
        if (GUI.Button(new Rect(500, 450, 200, 100), "+100000", buttonStyle))
        {
            gameData.money += 100000;
            RefreshPanel();
        }
#endif
    }

}