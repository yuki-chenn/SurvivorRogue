using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemPanel : MonoBehaviour
{

    private Button backBtn;

    private Transform content;

    private Text txtName;
    private Text txtDescription;
    private Text txtTitle;


    private List<WeaponTplInfo> weaponInfos;
    private List<ItemTplInfo> itemInfos;


    // 0:weapon,1:item
    private int showType = 0;

    private int curSelectedItemIndex = 0;
    private int curSelectedWeaponIndex = 0;

    private GameData gameData { get { return GameManager.Instance.gameData; } }

    private void Awake()
    {
        EventCenter.AddListener<string>(EventDefine.ShowWeaponItemPanel, Show);
        EventCenter.AddListener(EventDefine.HideWeaponItemPanel, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<string>(EventDefine.ShowWeaponItemPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideWeaponItemPanel, Hide);
    }

    void Start()
    {
        weaponInfos = TplUtil.GetWeaponList();
        weaponInfos.RemoveAll(x => x.Initial == 0);
        itemInfos = TplUtil.GetItemList();
        itemInfos.RemoveAll(x => x.Initial == 0);

        txtName = transform.Find("Des/Name").GetComponent<Text>();
        txtDescription = transform.Find("Des/DescriptionScroll/Viewport/Text").GetComponent<Text>();
        txtTitle = transform.Find("BgFrame/TitleBar/Title").GetComponent<Text>();

        backBtn = transform.Find("BgFrame/BackBtn").GetComponent<Button>();
        backBtn.onClick.AddListener(() =>
        {
            if (showType == 0)
            {
                gameData.initialWeaponId = weaponInfos[curSelectedWeaponIndex].ID;
                EventCenter.Broadcast(EventDefine.RefreshSelectWeapon);
            }
            else
            {
                gameData.initialItemId = itemInfos[curSelectedItemIndex].ID;
                EventCenter.Broadcast(EventDefine.RefreshSelectItem);
            }
            Hide();
        });

        gameObject.SetActive(false);
    }

    private void Hide()
    {
        //canvasGroup.DOFade(0, 0.5f).OnComplete(() => { gameObject.SetActive(false); });
        gameObject.SetActive(false);
    }
    
    private void Show(string type)
    {
        showType = type == "weapon" ? 0 : 1;
        InitScrollRect();
        gameObject.SetActive(true);
        
        //canvasGroup.DOFade(1, 1.5f);
    }

    private void InitScrollRect()
    {
        content = transform.Find("ScrollRect/Viewport/Content");
        if (showType == 0) InitWeaponScroll();
        else InitItemScroll();
        RefreshInfo();
    }

    private void InitWeaponScroll()
    {
        

        for (int i = 0; i < weaponInfos.Count; ++i)
        {
            var info = weaponInfos[i];
            Transform go = null;
            if(i >= content.childCount)
            {
                go = Instantiate(AssetManager.Instance.武器道具选择模板, content).transform;
            }
            else
            {
                go = content.GetChild(i);
            }
            
            go.Find("Icon").GetComponent<Image>().sprite =
                AssetManager.Instance.weaponSprite[info.Index];
            go.Find("Selected").gameObject.SetActive(curSelectedWeaponIndex == i);
            int index = i;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                curSelectedWeaponIndex = index;
                RefreshScroll();
                RefreshInfo();
            });
            go.name = string.Format("weapon-{0}-{1}", info.ID, info.Name);
            go.gameObject.SetActive(true);
        }
        for (int i = weaponInfos.Count; i < content.childCount; ++i)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void InitItemScroll()
    {
        for (int i = 0; i < itemInfos.Count; ++i)
        {
            var info = itemInfos[i];
            Transform go = null;
            if (i >= content.childCount)
            {
                go = Instantiate(AssetManager.Instance.武器道具选择模板, content).transform;
            }
            else
            {
                go = content.GetChild(i);
            }

            go.Find("Icon").GetComponent<Image>().sprite =
                AssetManager.Instance.itemSprite[info.Index];
            go.Find("Selected").gameObject.SetActive(curSelectedItemIndex == i);
            int index = i;
            go.GetComponent<Button>().onClick.RemoveAllListeners();
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                curSelectedItemIndex = index;
                RefreshScroll();
                RefreshInfo();
            });
            go.name = string.Format("item-{0}-{1}", info.ID, info.Name);
            go.gameObject.SetActive(true);
        }
        for(int i = itemInfos.Count; i < content.childCount; ++i)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void RefreshScroll()
    {
        int index = showType == 0 ? curSelectedWeaponIndex : curSelectedItemIndex;
        for (int i = 0; i < content.childCount; ++i)
        {
            var child = content.GetChild(i);
            child.Find("Selected").gameObject.SetActive(i == index);
        }
    }

    private void RefreshInfo()
    {
        if(showType == 0)
        {
            txtTitle.text = "请选择初始武器";
            var info = weaponInfos[curSelectedWeaponIndex];
            txtName.text = info.Name;
            txtDescription.text = info.Description;
        }
        else
        {
            txtTitle.text = "请选择初始道具";
            var info = itemInfos[curSelectedItemIndex];
            txtName.text = info.Name;
            txtDescription.text = info.Description;
        }
        
    }


}
