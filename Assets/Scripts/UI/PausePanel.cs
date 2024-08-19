using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Survivor.Utils;
using Survivor.Template;
using System.Collections;

public class PausePanel : MonoBehaviour
{
    private PlayerAttribute attr { get { return GameManager.Instance.Player.attr; } }

    private CanvasGroup canvasGroup;

    private Button btnBackGame;

    private List<Text> txtAttr = new List<Text>();

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowPausePanel, Show);
        EventCenter.AddListener(EventDefine.HidePausePanel, Hide);

        canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        btnBackGame = transform.Find("BackBtn").GetComponent<Button>();
        btnBackGame.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            Hide();
        });
        btnBackGame.interactable = false;

        Transform attrTrans = transform.Find("PlayerAttr");
        foreach (Transform child in attrTrans)
        {
            txtAttr.Add(child.Find("AttrValue").GetComponent<Text>());
        }

        gameObject.SetActive(false);
    }

    private void RefreshPanel()
    {
        RefreshPlayerAttr();
        RefreshItems();
        RefreshBuffs();
    }

    private void RefreshBuffs()
    {
        // ��ȡContent��Transform
        var content = transform.Find("BuffScrollRect/Viewport/Content");

        // ��ȡitemDic�ֵ�
        var buffList = GameManager.Instance.Player.buffList.buffs;

        // ����itemDic�ֵ䣬��̬��������ʾ����
        int index = 0;
        foreach (var buff in buffList)
        {
            GameObject buffGo;
            // ���content���������壬��������������
            if (index < content.childCount)
            {
                buffGo = content.GetChild(index).gameObject;
            }
            // ����ʵ����һ���µ�������
            else
            {
                buffGo = Instantiate(AssetManager.Instance.��ͣ����ģ��, content);
            }

            var buffInfo = TplUtil.GetBuffMap()[buff.ID];

            buffGo.transform.Find("BuffDes").GetComponent<Text>().text =
                string.Format("{0}{1}:{2}",
                buffInfo.Name,
                buffInfo.MaxStack == -1 && buff.curStack == 1 ? "" : "(" + buff.curStack + ")",
                buffInfo.Description
                );

            buffGo.SetActive(true);
            index++;
        }

        // ���ض����������
        for (int i = index; i < content.childCount; i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void RefreshItems()
    {
        // ��ȡContent��Transform
        var content = transform.Find("ItemScrollRect/Viewport/Content");

        // ��ȡitemDic�ֵ�
        var itemDic = GameManager.Instance.gameData.itemIDs;

        // ����itemDic�ֵ䣬��̬��������ʾ����
        int index = 0;
        foreach (var kvp in itemDic)
        {
            GameObject itemGo;
            // ���content���������壬��������������
            if (index < content.childCount)
            {
                itemGo = content.GetChild(index).gameObject;
            }
            // ����ʵ����һ���µ�������
            else
            {
                itemGo = Instantiate(AssetManager.Instance.��ͣ����ģ��, content);
            }

            var itemInfo = TplUtil.GetItemMap()[kvp.Key];
            itemGo.GetComponent<Image>().sprite = AssetManager.Instance.�������ߵȼ��߿�[itemInfo.Rank - 1];
            itemGo.transform.Find("Icon").GetComponent<Image>().sprite =
                AssetManager.Instance.itemSprite[itemInfo.Index];
            itemGo.transform.Find("Count").GetComponent<Text>().text = kvp.Value.ToString();
            itemGo.transform.Find("Bg").GetComponent<Image>().sprite = AssetManager.Instance.�������߱���[itemInfo.Rank - 1];

            itemGo.SetActive(true);
            index++;
        }

        // ���ض����������
        for (int i = index; i < content.childCount; i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void RefreshPlayerAttr()
    {
        if(GameManager.Instance.fsm.CurrentStateID == StateID.FightState)
        {
            txtAttr[0].text = attr.�������.ToString("F0");
            txtAttr[1].text = attr.�����ٶ�.ToString("F0");
            txtAttr[2].text = attr.����.ToString("F0");
            txtAttr[3].text = attr.����.ToString("F0");
            txtAttr[4].text = attr.����.ToString("F0");
            txtAttr[5].text = attr.����.ToString("F1") + "%";
            txtAttr[6].text = attr.������.ToString("F1") + "%";
            txtAttr[7].text = attr.�����˺�.ToString("F1") + "%";
            txtAttr[8].text = attr.�ƶ��ٶ�.ToString("F0");
            txtAttr[9].text = attr.����.ToString("F0");
            txtAttr[10].text = GameManager.Instance.gameData.salary.ToString();
        }

        if (GameManager.Instance.fsm.CurrentStateID == StateID.BuyState)
        {
            txtAttr[0].text = GameManager.Instance.gameData.playerAttr.�������.ToString("F0");
            txtAttr[1].text = GameManager.Instance.gameData.playerAttr.�����ٶ�.ToString("F0");
            txtAttr[2].text = GameManager.Instance.gameData.playerAttr.����.ToString("F0");
            txtAttr[3].text = GameManager.Instance.gameData.playerAttr.����.ToString("F0");
            txtAttr[4].text = GameManager.Instance.gameData.playerAttr.����.ToString("F0");
            txtAttr[5].text = GameManager.Instance.gameData.playerAttr.����.ToString("F1") + "%";
            txtAttr[6].text = GameManager.Instance.gameData.playerAttr.������.ToString("F1") + "%";
            txtAttr[7].text = GameManager.Instance.gameData.playerAttr.�����˺�.ToString("F1") + "%";
            txtAttr[8].text = GameManager.Instance.gameData.playerAttr.�ƶ��ٶ�.ToString("F0");
            txtAttr[9].text = GameManager.Instance.gameData.playerAttr.����.ToString("F0");
            txtAttr[10].text = GameManager.Instance.gameData.salary.ToString();
        }


    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowPausePanel, Show);
        EventCenter.RemoveListener(EventDefine.HidePausePanel, Hide);
    }

    void Start()
    {
        
    }

    private void Hide()
    {
        if (GameManager.Instance.fsm.CurrentStateID == StateID.FightState) Time.timeScale = 1;
        btnBackGame.interactable = false;
        //canvasGroup.DOFade(0, 0.5f).OnComplete(() => 
        //{
        //GameManager.Instance.fsm.PerformTransition(Transition.Restart);
        GameUIManager.Instance.OnPausePanelHide();
        gameObject.SetActive(false);
        //});
    }

    private void Show()
    {
        RefreshPanel();
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, 0.2f).OnComplete(()=>
        {
            if (GameManager.Instance.fsm.CurrentStateID == StateID.FightState) Time.timeScale = 0;
            StartCoroutine(CanClose());
        });
    }

    IEnumerator CanClose()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        btnBackGame.interactable = true;
    }

}