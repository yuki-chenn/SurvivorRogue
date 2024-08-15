using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private Text txtLoadPercentage;
    private Text txtLoading;
    private Slider sliderLoadingBar;

    private void Start()
    {
        txtLoadPercentage = transform.Find("LoadingPercentTxt").GetComponent<Text>();
        txtLoading = transform.Find("LoadingTxt").GetComponent<Text>();
        sliderLoadingBar = transform.Find("LoadingBar").GetComponent<Slider>();
        StartCoroutine(LoadingScene("GameScene"));
    }


    IEnumerator LoadingScene(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        while(!op.isDone)
        {
            sliderLoadingBar.value = op.progress;
            txtLoadPercentage.text = string.Format("{0}%", (op.progress * 100).ToString("F0"));

            // ╪сть
            if(op.progress >= 0.9f)
            {
                sliderLoadingBar.value = 1;
                txtLoadPercentage.text = "100%";

#if UNITY_EDITOR
                txtLoading.text = "Press anykey to start ...";
                if (Input.anyKeyDown)
                {
                    op.allowSceneActivation = true;
                }
#else
                txtLoading.text = "Click anywhere to start ...";
                if (Input.touchCount > 0)
                {
                    op.allowSceneActivation = true;
                }

#endif
            }
            yield return null;
        }
    }


}
