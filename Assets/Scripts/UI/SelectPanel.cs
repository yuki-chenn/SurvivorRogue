using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Survivor.Utils;
using System;
using Survivor.Template;

public class SelectPanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private Button btnBack;
    private Button btnStart;

    private Transform heroContent;

    private Text txtHeroName;
    private Text txtHeroDes;
    private Text txtSkillDes;
    private Text txtPassiveDes;

    private Image imgSkillIcon;
    private Image imgPassiveIcon;

    private Button btnSelectWeapon;
    private Button btnSelectItem;

    private Image imgSelectWeapon;
    private Image imgSelectItem;


    private List<HeroTplInfo> heroList;
    private int curSelectedHeroIndex = 0;

    private GameData gameData { get { return GameManager.Instance.gameData; } }


    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowSelectPanel, Show);
        EventCenter.AddListener(EventDefine.HideSelectPanel, Hide);


        EventCenter.AddListener(EventDefine.RefreshSelectWeapon, RefreshSelectWeapon);
        EventCenter.AddListener(EventDefine.RefreshSelectItem, RefreshSelectItem);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowSelectPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideSelectPanel, Hide);


        EventCenter.RemoveListener(EventDefine.RefreshSelectWeapon, RefreshSelectWeapon);
        EventCenter.RemoveListener(EventDefine.RefreshSelectItem, RefreshSelectItem);
    }

    void Start()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        InitHeroScrollRect();
        InitDescription();
        InitButton();

        imgSelectWeapon = transform.Find("WeaponItemSelect/Weapon/SelectWeaponBtn").GetComponent<Image>();
        imgSelectItem = transform.Find("WeaponItemSelect/Item/SelectItemBtn").GetComponent<Image>();

        gameObject.SetActive(false);
    }

    private void InitButton()
    {
        btnBack = transform.Find("BackBtn").GetComponent<Button>();
        btnBack.onClick.AddListener(() =>
        {
            Hide();
            EventCenter.Broadcast(EventDefine.ShowMainMenuPanel);
        });

        btnStart = transform.Find("StartBtn").GetComponent<Button>();
        btnStart.onClick.AddListener(() =>
        {
            if (gameData.initialItemId == -1 || gameData.initialWeaponId == -1) return;
            
            // 只在GameManager中记录选择了什么角色，携带了什么武器和道具等等，
            gameData.selectHeroId = heroList[curSelectedHeroIndex].ID;
            //gameData.playerPrefab = AssetManager.Instance.HeroPrefab[curSelectedHeroIndex];

            // 设置人物属性
            gameData.SetInitialPlayerAttr();

            // 设置武器道具
            if (gameData.itemIDs.Count != 0) gameData.itemIDs.Clear();
            gameData.itemIDs[gameData.initialItemId] = 1;
            gameData.weaponIDs[0] = gameData.initialWeaponId;

            // 进入选择存档的界面
            EventCenter.Broadcast<int>(EventDefine.ShowDataFilePanel, 0);
            Hide();
            //SceneManager.LoadScene("LoadScene");
        });

        btnSelectWeapon = transform.Find("WeaponItemSelect/Weapon/SelectWeaponBtn").GetComponent<Button>();
        btnSelectWeapon.onClick.AddListener(() =>
        {
            EventCenter.Broadcast<string>(EventDefine.ShowWeaponItemPanel, "weapon");
        });
        btnSelectItem = transform.Find("WeaponItemSelect/Item/SelectItemBtn").GetComponent<Button>();
        btnSelectItem.onClick.AddListener(() =>
        {
            EventCenter.Broadcast<string>(EventDefine.ShowWeaponItemPanel, "item");
        });

        
    }

    private void InitDescription()
    {
        txtHeroName = transform.Find("HeroDes/Name").GetComponent<Text>();
        txtHeroDes = transform.Find("HeroDes/DescriptionScroll/Viewport/Text").GetComponent<Text>();
        txtSkillDes = transform.Find("SkillDes/Skill/DescriptionScroll/Viewport/Text").GetComponent<Text>();
        txtPassiveDes = transform.Find("SkillDes/Passive/DescriptionScroll/Viewport/Text").GetComponent<Text>();
        imgSkillIcon = transform.Find("SkillDes/Skill/SkillIcon").GetComponent<Image>();
        imgPassiveIcon = transform.Find("SkillDes/Passive/PassiveIcon").GetComponent<Image>();

        RefreshHeroInfo();
    }


    private void InitHeroScrollRect()
    {
        heroContent = transform.Find("HeroScrollRect/Viewport/Content");

        heroList = TplUtil.GetHeroList();

        for(int i = 0;i < heroList.Count; ++i)
        {
            var info = heroList[i];
            var hero = Instantiate(AssetManager.Instance.英雄选择头像模板, heroContent);
            hero.transform.Find("Avatar").GetComponent<Image>().sprite = 
                AssetManager.Instance.heroSprite[info.Index];
            hero.transform.Find("Selected").gameObject.SetActive(curSelectedHeroIndex == i);
            int index = i;
            hero.GetComponent<Button>().onClick.AddListener(() =>
            {
                curSelectedHeroIndex = index;
                RefreshHeroScroll();
                RefreshHeroInfo();
            });
            hero.name = string.Format("{0}-{1}", info.ID, info.Name);
        }


    }

    private void RefreshHeroScroll()
    {
        for(int i = 0; i < heroContent.childCount; ++i)
        {
            var child = heroContent.GetChild(i);
            child.Find("Selected").gameObject.SetActive(i == curSelectedHeroIndex);
        }
    }

    private void RefreshHeroInfo()
    {
        var info = heroList[curSelectedHeroIndex];
        txtHeroName.text = info.Name;
        txtHeroDes.text = info.HeroDescription;
        txtSkillDes.text = info.SkillDescription;
        txtPassiveDes.text = info.PassiveDescription;

        imgSkillIcon.sprite = AssetManager.Instance.skillSprite[info.Index];
        imgPassiveIcon.sprite = AssetManager.Instance.passiveSprite[info.Index];
    }

    private void RefreshSelectWeapon()
    {
        var weaponInfo = TplUtil.GetWeaponMap()[gameData.initialWeaponId];
        imgSelectWeapon.sprite = AssetManager.Instance.weaponSprite[weaponInfo.Index];
    }

    private void RefreshSelectItem()
    {
        var itemInfo = TplUtil.GetItemMap()[gameData.initialItemId];
        imgSelectItem.sprite = AssetManager.Instance.itemSprite[itemInfo.Index];
    }

    private void Hide()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(()=> { gameObject.SetActive(false); });
    }

    private void Show()
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, 1.5f);
    }

}
