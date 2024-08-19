using Survivor.Base;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public float effectScale = 0.6f;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // 设置背景音乐音频源循环播放
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
        if (audioSource.clip == clip)
        {
            return; // 如果当前播放的音频已经是目标音频，不重复播放
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void PlayEffectAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, effectScale);
    }
}
