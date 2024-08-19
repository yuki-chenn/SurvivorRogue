using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Survivor.Template;
using Survivor.Utils;
using TMPro;

public class OpenChestPanel : MonoBehaviour
{
    private GameData gameData { get { return GameManager.Instance.gameData; } }

    private Transform itemTrans;

    private Button btnKeep;
    private Button btnSale;

    private Image imgIcon;
    private Image imgIconBg;
    private Image imgIconBorder;
    private Image imgItem;
    private Image imgBorder;
    private TextMeshProUGUI txtSalePrice;
    private Text txtName;
    private Text txtRank;
    private Text txtDescription;

    private int[] chestCount;

    private int curItemId = -1; 
    private ItemTplInfo curItemInfo
    {
        get
        {
            if (curItemId == -1) return null;
            else return TplUtil.GetItemMap()[curItemId];
        }
    }

    private void Awake()
    {
        EventCenter.AddListener<int[]>(EventDefine.ShowOpenChestPanel, Show);
        EventCenter.AddListener(EventDefine.HideOpenChestPanel, Hide);

        itemTrans = transform.Find("OpenChestItem");

        Transform item = itemTrans.Find("Item");
        imgItem = item.GetComponent<Image>();
        imgBorder = item.Find("Border").GetComponent<Image>();
        imgIcon = item.Find("Icon/ItemIcon").GetComponent<Image>();
        imgIconBorder = item.Find("Icon").GetComponent<Image>();
        imgIconBg = item.Find("Icon/Bg").GetComponent<Image>();
        txtName = item.Find("Name").GetComponent<Text>();
        txtRank = item.Find("Rank").GetComponent<Text>();
        txtDescription = item.Find("DescriptionScroll/Viewport/Text").GetComponent<Text>();

        Transform buttons = itemTrans.Find("Btns");
        txtSalePrice = buttons.Find("SaleBtn/Sale").GetComponent<TextMeshProUGUI>();

        btnKeep = buttons.Find("KeepBtn").GetComponent<Button>();
        btnKeep.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            // 保留该道具
            GameManager.Instance.GetItem(curItemInfo);
            itemTrans.gameObject.SetActive(false);
            EventCenter.Broadcast(EventDefine.OnChestItemSelect);
        });

        btnSale = buttons.Find("SaleBtn").GetComponent<Button>();
        btnSale.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            // 出售该道具
            GameManager.Instance.gameData.money += Constants.CHEST_SALE_PRICE_BY_RANK[curItemInfo.Rank];
            itemTrans.gameObject.SetActive(false);
            EventCenter.Broadcast(EventDefine.OnChestItemSelect);
        });

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int[]>(EventDefine.ShowOpenChestPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideOpenChestPanel, Hide);
    }

    void Start()
    {
        
    }

    private void RefreshItemInfo()
    {
        if (curItemId == -1) return;
        imgItem.color = Constants.RANK_COLOR_BG[curItemInfo.Rank];
        imgBorder.sprite = AssetManager.Instance.商店武器道具购买等级边框[curItemInfo.Rank - 1];
        imgIcon.sprite = AssetManager.Instance.itemSprite[curItemInfo.Index];
        imgIconBg.sprite = AssetManager.Instance.武器道具背景[curItemInfo.Rank - 1];
        imgIconBorder.sprite = AssetManager.Instance.武器道具等级边框[curItemInfo.Rank - 1];
        txtName.text = curItemInfo.Name;
        txtRank.text = Constants.RANK_NAME[curItemInfo.Rank];
        txtRank.color = Constants.RANK_COLOR[curItemInfo.Rank];
        txtDescription.text = curItemInfo.Description;
        txtSalePrice.SetText(Constants.TMP_IMG_PREFIX + string.Format("{0}", Constants.CHEST_SALE_PRICE_BY_RANK[curItemInfo.Rank]));
        itemTrans.gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        GameManager.Instance.gameData.ClearChestCount();
    }

    private void Show(int[] count)
    {
        chestCount = count;
        gameObject.SetActive(true);
        StartCoroutine(ShowChestItems());
    }

    IEnumerator ShowChestItems()
    {
        for (int rank = 2; rank <= 4; ++rank)
        {
            for (int i = 0; i < chestCount[rank - 2]; ++i)
            {
                if(rank == 2)
                {
                    bool isRare = RandomUtil.IsProbabilityMet(0.6f);
                    curItemId = isRare ? GameManager.Instance.GetRandomItem(RankType.Rare) :
                        GameManager.Instance.GetRandomItem(RankType.Normal);
                }
                else
                {
                    curItemId = GameManager.Instance.GetRandomItem((RankType)rank);
                }
                
                RefreshItemInfo();
                yield return new WaitUntil(() => !itemTrans.gameObject.activeSelf);
            }
        }
        Hide();
    }

}
