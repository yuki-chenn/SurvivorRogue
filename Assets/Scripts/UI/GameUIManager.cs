using Survivor.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{

    // 释放技能
    private Button btnUseSkill;
    private Text txtSkillCountdown;
    private Image imgSkillMask;
    private Image imgSkillIcon;

    // 波次UI
    private Text txtWaveCountDown;
    private Text txtWaveCount;

    // 金钱
    private Text txtMoney;

    // 等级经验
    private Text txtLevel;
    private Text txtExp;
    private Slider sliderExp;
    private Image imgAvatar;
    private Button btnInfo;

    protected override void Awake()
    {
        base.Awake();
        InitSkillUI();
        InitExpLevelUI();

        txtWaveCount = transform.Find("WaveCount").GetComponent<Text>();
        txtWaveCountDown = transform.Find("WaveCountDown").GetComponent<Text>();
        txtMoney = transform.Find("Money").GetComponent<Text>();
    }

    private void InitSkillUI()
    {
        Transform skillTrans = transform.Find("Skill");
        btnUseSkill = skillTrans.Find("useSkill").GetComponent<Button>();
        btnUseSkill.onClick.AddListener(() =>
        {
            GameManager.Instance.playerGo.GetComponent<Player>().使用技能();
        });

        txtSkillCountdown = skillTrans.Find("Mask/Countdown").GetComponent<Text>();
        imgSkillMask = skillTrans.Find("Mask").GetComponent<Image>();
        imgSkillIcon = skillTrans.Find("useSkill/SkillIcon").GetComponent<Image>();
        imgSkillIcon.sprite = AssetManager.Instance.skillSprite[GameManager.Instance.gameData.HeroInfo.Index];
    }

    private void InitExpLevelUI()
    {
        Transform ExpLevel = transform.Find("ExpLevel");
        txtLevel = ExpLevel.Find("Level").GetComponent<Text>();
        txtExp = ExpLevel.Find("Exp").GetComponent<Text>();
        sliderExp = ExpLevel.Find("ExpSlider").GetComponent<Slider>();
        imgAvatar = ExpLevel.Find("Avatar").GetComponent<Image>();
        imgAvatar.sprite = AssetManager.Instance.heroSprite[GameManager.Instance.gameData.HeroInfo.Index];
        btnInfo = ExpLevel.Find("Avatar").GetComponent<Button>();
        btnInfo.onClick.AddListener(() =>
        {
            btnInfo.interactable = false;
            //GameManager.Instance.fsm.PerformTransition(Transition.Pause);
            EventCenter.Broadcast(EventDefine.ShowPausePanel);
        });

    }

    public void OnPausePanelHide()
    {
        btnInfo.interactable = true;
    }

    public void UpdateSkillCountdown(float lastTime,float ratio)
    {
        if (lastTime <= 0)
        {
            lastTime = 0;
            btnUseSkill.enabled = true;
            if (imgSkillMask.gameObject.activeSelf)
            {
                imgSkillMask.gameObject.SetActive(false);
            }
        }
        else
        {
            btnUseSkill.enabled = false;
            if (!imgSkillMask.gameObject.activeSelf)
            {
                imgSkillMask.gameObject.SetActive(true);
            }
        }
        txtSkillCountdown.text = lastTime.ToString("F1");
        imgSkillMask.fillAmount = ratio;
    }

    public void UpdateWaveCountDown(float clk)
    {
        txtWaveCountDown.text = clk.ToString("F0"); 
    }
    public void UpdateWaveCount(int curWave)
    {
        txtWaveCount.text = string.Format("wave-{0}", curWave);
    }

    public void UpdateMoney(int money)
    {
        txtMoney.text = string.Format("金钱：{0}", money.ToString());
    }

    public void UpdateLevelExp(int level,int curExp,int nextExp)
    {
        txtLevel.text = string.Format("Level：{0}", level.ToString());
        txtExp.text = string.Format("{0}/{1}", curExp.ToString(), nextExp.ToString());
        sliderExp.value = 1.0f * curExp / nextExp;
    }
}
