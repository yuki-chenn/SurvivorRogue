using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameClearPanel : MonoBehaviour
{

    private Button btnBackMenu;
    private Button btnEndlessGame;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowGameClearPanel, Show);
        EventCenter.AddListener(EventDefine.HideGameClearPanel, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGameClearPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideGameClearPanel, Hide);
    }

    void Start()
    {
        btnBackMenu = transform.Find("NewGameBtn").GetComponent<Button>();
        btnBackMenu.onClick.AddListener(() =>
        {
            GameManager.Instance.ClearGameDataAndPlayerPrefs(GameManager.Instance.gameData.saveIndex);
            GameManager.Instance.fsm.PerformTransition(Transition.Confirm);
            SceneManager.LoadScene("MenuScene");
        });
        btnEndlessGame = transform.Find("EndlessGameBtn").GetComponent<Button>();
        btnEndlessGame.onClick.AddListener(() =>
        {
            // 进入无尽模式
            Hide();
            GameManager.Instance.fsm.PerformTransition(Transition.Endless);
        });

        gameObject.SetActive(false);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

}
