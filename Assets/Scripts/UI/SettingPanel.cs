using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{

    private Button backBtn;

    private Slider effectSlider;
    private Slider bgmSlider;

    private Text txtBgmValue;
    private Text txtEffectValue;

    private float bgmScale = 1.0f;
    private float effectScale = 1.0f;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowSettingPanel, Show);
        EventCenter.AddListener(EventDefine.HideSettingPanel, Hide);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowSettingPanel, Show);
        EventCenter.RemoveListener(EventDefine.HideSettingPanel, Hide);
    }

    void Start()
    {
        effectSlider = transform.Find("SettingList/EffectItem/Slider").GetComponent<Slider>();
        bgmSlider = transform.Find("SettingList/BgmItem/Slider").GetComponent<Slider>();

        txtBgmValue = transform.Find("SettingList/BgmItem/TxtBgmValue").GetComponent<Text>();
        txtEffectValue = transform.Find("SettingList/EffectItem/TxtEffectValue").GetComponent<Text>();

        backBtn = transform.Find("BgFrame/BackBtn").GetComponent<Button>();
        backBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonCliclkEffect();
            AudioManager.Instance.SetBgmScale(bgmScale);
            AudioManager.Instance.SetEffectScale(effectScale);
            Hide();
        });

        bgmSlider.onValueChanged.AddListener((value) =>
        {
            bgmScale = value;
            Refresh();
        });

        effectSlider.onValueChanged.AddListener((value) =>
        {
            effectScale = value;
            Refresh();
        });

        gameObject.SetActive(false);
    }

    private void Hide()
    {
        //canvasGroup.DOFade(0, 0.5f).OnComplete(() => { gameObject.SetActive(false); });
        gameObject.SetActive(false);
    }
    
    private void Show()
    {
        bgmScale = AudioManager.Instance.bgmScale;
        effectScale = AudioManager.Instance.effectScale;
        Refresh();
        gameObject.SetActive(true);
    }

    private void Refresh()
    {
        int bgmValue = (int)(bgmScale * 100);
        int effectValue = (int)(effectScale * 100);

        transform.Find("SettingList/EffectItem/IconOn").gameObject.SetActive(effectValue != 0);
        transform.Find("SettingList/EffectItem/IconOff").gameObject.SetActive(effectValue == 0);

        transform.Find("SettingList/BgmItem/IconOn").gameObject.SetActive(bgmValue != 0);
        transform.Find("SettingList/BgmItem/IconOff").gameObject.SetActive(bgmValue == 0);

        bgmSlider.value = bgmScale;
        effectSlider.value = effectScale;

        txtBgmValue.text = bgmValue.ToString();
        txtEffectValue.text = effectValue.ToString();
    }


}
