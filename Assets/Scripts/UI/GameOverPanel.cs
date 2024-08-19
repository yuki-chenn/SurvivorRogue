using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{

    private Button btnBackMenu;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowGameOverPanel, Show);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGameOverPanel, Show);
    }

    void Start()
    {
        btnBackMenu = transform.Find("NewGameBtn").GetComponent<Button>();
        btnBackMenu.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            GameManager.Instance.ClearGameDataAndPlayerPrefs(GameManager.Instance.gameData.saveIndex);
            GameManager.Instance.fsm.PerformTransition(Transition.Confirm);
            SceneManager.LoadScene("MenuScene");
        });

        gameObject.SetActive(false);
    }

    private void Hide()
    {
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

}
