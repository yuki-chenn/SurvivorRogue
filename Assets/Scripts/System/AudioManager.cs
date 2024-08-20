using Survivor.Base;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public float effectScale = 1.0f;
    public float bgmScale = 1.0f;

    private AudioSource bgmAudioSource;
    private AudioSource effectAudioSource;

    public AudioClip 主界面bgm;
    public AudioClip 商店界面bgm;
    public AudioClip 游戏界面bgm;

    public AudioClip 按钮点击effect;
    public AudioClip 买东西effect;
    public AudioClip 锁东西effect;
    public AudioClip 刷新商店effect;
    public AudioClip 升级effect;
    public AudioClip 升级武器effect;
    public AudioClip 错误effect;

    protected override void Awake()
    {
        base.Awake();
        var ass = GetComponents<AudioSource>();
        bgmAudioSource = ass[0];
        effectAudioSource = ass[1];
        bgmAudioSource.loop = true; // 设置背景音乐音频源循环播放
        LoadPlayerPrefs();
        SetBgmScale(bgmScale);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetFloat("bgmScale", bgmScale);
        PlayerPrefs.SetFloat("effectScale", effectScale);
        PlayerPrefs.Save();
    }

    public void LoadPlayerPrefs()
    {
        bgmScale = PlayerPrefs.HasKey("bgmScale") ? PlayerPrefs.GetFloat("bgmScale") : 1.0f;
        effectScale = PlayerPrefs.HasKey("effectScale") ? PlayerPrefs.GetFloat("effectScale") : 1.0f;
    }

    public void SetBgmScale(float scale)
    {
        bgmScale = scale;
        bgmAudioSource.volume = scale;
    }

    public void SetEffectScale(float scale)
    {
        effectScale = scale;
    }

    // 播放主界面BGM
    public void PlayMainMenuBGM()
    {
        PlayBgmAudio(主界面bgm);
    }

    // 播放商店界面BGM
    public void PlayShopBGM()
    {
        PlayBgmAudio(商店界面bgm);
    }

    // 播放游戏界面BGM
    public void PlayGameBGM()
    {
        PlayBgmAudio(游戏界面bgm);
    }

    public void PlayButtonCliclkEffect()
    {
        PlayEffectAudio(按钮点击effect);
    }

    public void PlayBuySelectionsEffect()
    {
        PlayEffectAudio(买东西effect);
    }

    public void PlayRefreshSelectionsEffect()
    {
        PlayEffectAudio(刷新商店effect);
    }
    public void PlayLockSelectionsEffect()
    {
        PlayEffectAudio(锁东西effect);
    }

    public void PlayLevelUpEffect()
    {
        PlayEffectAudio(升级effect);
    }

    public void PlayLevelUpWeaponEffect()
    {
        PlayEffectAudio(升级武器effect);
    }

    public void PlayErrorEffect()
    {
        PlayEffectAudio(错误effect);
    }

    // 通用播放BGM方法
    private void PlayBgmAudio(AudioClip clip)
    {
        if (bgmAudioSource.clip == clip)
        {
            return; // 如果当前播放的音频已经是目标音频，不重复播放
        }

        bgmAudioSource.clip = clip;
        bgmAudioSource.Play();
    }

    private void PlayEffectAudio(AudioClip clip)
    {
        effectAudioSource.PlayOneShot(clip, effectScale);
    }

    public void PlayEnemyAudio(AudioClip clip)
    {
        effectAudioSource.PlayOneShot(clip, effectScale);
    }
}
