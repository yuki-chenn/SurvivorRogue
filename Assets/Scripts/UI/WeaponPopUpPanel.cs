using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Survivor.Template;
using Survivor.Utils;
using TMPro;

public class WeaponPopUpPanel : MonoBehaviour
{
    private GameData gameData { get { return GameManager.Instance.gameData; } }

    private Transform popup;

    private Button btnBack;

    private Button btnRankup;
    private Button btnSale;

    private Image imgIcon;
    private Image imgIconBg;
    private Image imgIconBorder;
    private TextMeshProUGUI txtSalePrice;
    private Text txtName;
    private Text txtDescription;
    private Text txtRank;

    private int weaponIndex;



    private void Awake()
    {
        EventCenter.AddListener<WeaponTplInfo,Vector2,int>(EventDefine.ShowWeaponPopUpPanel, Show);
        EventCenter.AddListener(EventDefine.HideWeaponPopUpPanel, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<WeaponTplInfo, Vector2,int>(EventDefine.ShowWeaponPopUpPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideWeaponPopUpPanel, Hide);
    }

    void Start()
    {
        popup = transform.Find("PopUp");

        Transform item = popup.Find("Item");
        imgIcon = item.Find("Icon/ItemIcon").GetComponent<Image>();
        imgIconBorder = item.Find("Icon").GetComponent<Image>();
        imgIconBg = item.Find("Icon/Bg").GetComponent<Image>();
        txtName = item.Find("Name").GetComponent<Text>();
        txtDescription = item.Find("DescriptionScroll/Viewport/Text").GetComponent<Text>();
        txtRank = item.Find("Rank").GetComponent<Text>();

        Transform buttons = popup.Find("Btns");
        txtSalePrice = buttons.Find("SaleBtn/Sale").GetComponent<TextMeshProUGUI>();

        btnRankup = buttons.Find("RankupBtn").GetComponent<Button>();
        btnRankup.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayLevelUpWeaponEffect();
            if (!GameManager.Instance.CanRankUp(weaponIndex)) return;
            // 升级
            //Debug.Log("升级");
            GameManager.Instance.RankUpWeapon(weaponIndex);
            EventCenter.Broadcast(EventDefine.OnWeaponRanked);
            Hide();
        });

        btnSale = buttons.Find("SaleBtn").GetComponent<Button>();
        btnSale.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayBuySelectionsEffect();
            // 出售
            //Debug.Log("出售");
            GameManager.Instance.SaleWeapon(weaponIndex);
            Hide();
            EventCenter.Broadcast(EventDefine.OnWeaponSaled);
        });
        


        btnBack = transform.GetComponent<Button>();
        btnBack.onClick.AddListener(() =>
        {
            Hide();
        });

        Hide();
    }

    private void RefreshInfo(WeaponTplInfo info)
    {
        btnRankup.gameObject.SetActive(GameManager.Instance.CanRankUp(weaponIndex));
        imgIcon.sprite = AssetManager.Instance.weaponSprite[info.Index];
        imgIconBg.sprite = AssetManager.Instance.武器道具背景[info.Rank - 1];
        imgIconBorder.sprite = AssetManager.Instance.武器道具等级边框[info.Rank - 1];
        txtName.text = info.Name;
        txtDescription.text = info.Description;
        txtRank.text = Constants.RANK_NAME[info.Rank];
        txtRank.color = Constants.RANK_COLOR[info.Rank];
        txtSalePrice.SetText(Constants.TMP_IMG_PREFIX + string.Format("{0}", (int)(info.Price * gameData.saleDiscount)));
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show(WeaponTplInfo info,Vector2 pos,int index)
    {
        weaponIndex = index;
        popup.position = pos;
        RefreshInfo(info);
        gameObject.SetActive(true);
        
    }

}
