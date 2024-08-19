using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuPanel : MonoBehaviour
{
    private RectTransform titleRecT;
    private RectTransform buttonsRecT;

    private Button btnNewGame;
    private Button btnLoadGame;
    private Button btnExitGame;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowMainMenuPanel, Show);
        EventCenter.AddListener(EventDefine.HideMainMenuPanel, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainMenuPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideMainMenuPanel, Hide);
    }

    void Start()
    {
        titleRecT = transform.Find("Title") as RectTransform;
        buttonsRecT = transform.Find("Buttons") as RectTransform;

        Transform buttons = transform.Find("Buttons");
        btnNewGame = buttons.Find("NewGameBtn").GetComponent<Button>();
        btnNewGame.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            Hide();
            EventCenter.Broadcast(EventDefine.ShowSelectPanel);
        });

        btnLoadGame = buttons.Find("LoadGameBtn").GetComponent<Button>();
        btnLoadGame.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            Hide();
            EventCenter.Broadcast<int>(EventDefine.ShowDataFilePanel, 1);
        });

        btnExitGame = buttons.Find("ExitGameBtn").GetComponent<Button>();
        btnExitGame.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
#if UNITY_EDITOR  
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });

        Show();
    }

    private void Hide()
    {
        titleRecT.DOAnchorPos(new Vector2(0, 380), 0.5f);
        buttonsRecT.DOAnchorPos(new Vector2(0, -80), 0.5f).OnComplete(() => { gameObject.SetActive(false); });
    }

    private void Show()
    {
        gameObject.SetActive(true);
        titleRecT.DOAnchorPos(new Vector2(0,-370), 0.5f);
        buttonsRecT.DOAnchorPos(new Vector2(0, 222), 0.5f);
    }

}
