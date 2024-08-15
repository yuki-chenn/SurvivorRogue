using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Survivor.Utils;

public class DataFilePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private Text txtTitle;
    private Button btnBack;

    private Transform dataFileContent;

    
    private bool isLoad;

    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.ShowDataFilePanel, Show);
        EventCenter.AddListener(EventDefine.HideDataFilePanel, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.ShowDataFilePanel, Show);
        EventCenter.RemoveListener(EventDefine.HideDataFilePanel, Hide);
    }

    void Start()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        txtTitle = transform.Find("Title").GetComponent<Text>();
        btnBack = transform.Find("BackBtn").GetComponent<Button>();
        btnBack.onClick.AddListener(() =>
        {
            if(isLoad) EventCenter.Broadcast(EventDefine.ShowMainMenuPanel);
            else EventCenter.Broadcast(EventDefine.ShowSelectPanel);
            Hide();
        });

        InitHeroScrollRect();

        gameObject.SetActive(false);
    }

    private void InitHeroScrollRect()
    {
        dataFileContent = transform.Find("DataFileScrollRect/Viewport/Content");

        for (int i = 0; i < Constants.MAX_DATAFILE_SLOT; ++i)
        {
            var file = Instantiate(AssetManager.Instance.数据存档模板, dataFileContent);
            int index = i;
            file.transform.Find("FileData/BtnDel").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.Instance.ClearGameDataAndPlayerPrefs(index);
                RefreshScroll();
            });
            
            file.GetComponent<Button>().onClick.AddListener(() =>
            {
                Debug.Log(string.Format("{0}了存档{1}", isLoad ? "读取" : "保存", index));
                SaveOrLoadData(index);
                RefreshScroll();
            });
            file.name = string.Format("datafile-{0}",index);
        }
        RefreshScroll();
    }

    private void SaveOrLoadData(int dataIndex)
    {
        GameManager.Instance.gameData.saveIndex = dataIndex;
        if (isLoad)
        {
            GameManager.Instance.LoadGameData(dataIndex);
            GameManager.Instance.fsm.PerformTransition(Transition.LoadGame);
            SceneManager.LoadScene("LoadScene");
        }
        else
        {
            GameManager.Instance.SaveGameData(dataIndex);

            GameManager.Instance.SavePlayerPrefs(dataIndex);

            SceneManager.LoadScene("LoadScene");
        }
    }

    private void RefreshScroll()
    {
        for (int i = 0; i < Constants.MAX_DATAFILE_SLOT; ++i)
        {
            var file = dataFileContent.GetChild(i);

            var data = GameManager.Instance.LoadPlayerPrefs(i);
            if (data == null)
            {
                file.transform.Find("IsEmpty").gameObject.SetActive(true);
                file.transform.Find("FileData").gameObject.SetActive(false);
                file.GetComponent<Button>().interactable = !isLoad;
            }
            else
            {
                file.transform.Find("FileData").gameObject.SetActive(true);
                file.transform.Find("FileData/SaveTime").GetComponent<Text>().text = data.saveTime;
                file.transform.Find("FileData/Level").GetComponent<Text>().text = string.Format("Level {0}", data.level);
                file.transform.Find("FileData/Money").GetComponent<Text>().text = string.Format("money : {0}", data.money);
                file.transform.Find("FileData/CurWave").GetComponent<Text>().text = string.Format("wave {0}", data.waveCount);
                file.transform.Find("FileData/PlayingTime").GetComponent<Text>().text = CommonUtil.SecondsToTimeFormat(data.playTime);
                file.transform.Find("FileData/EndlessWave").gameObject.SetActive(data.isEndless);
                file.transform.Find("FileData/SelectHeroIcon").GetComponent<Image>().sprite = AssetManager.Instance.heroSprite[data.selectHeroSpriteIndex];
                file.transform.Find("IsEmpty").gameObject.SetActive(false);
                file.GetComponent<Button>().interactable = true;
            }
        }
    }

    private void Hide()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() => { gameObject.SetActive(false); });
    }

    private void Show(int isLoad)
    {
        this.isLoad = isLoad == 1;
        txtTitle.text = isLoad == 0 ? "请选择你要保存的存档" : "请选择你要读取的存档";
        RefreshScroll();
        gameObject.SetActive(true);
        canvasGroup.DOFade(1, 1.5f);
    }

}
