using Survivor.Base;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public float effectScale = 1.0f;
    public float bgmScale = 1.0f;

    private AudioSource bgmAudioSource;
    private AudioSource effectAudioSource;

    public AudioClip ������bgm;
    public AudioClip �̵����bgm;
    public AudioClip ��Ϸ����bgm;

    public AudioClip ��ť���effect;
    public AudioClip ����effect;
    public AudioClip ������effect;
    public AudioClip ˢ���̵�effect;
    public AudioClip ����effect;
    public AudioClip ��������effect;
    public AudioClip ����effect;

    protected override void Awake()
    {
        base.Awake();
        var ass = GetComponents<AudioSource>();
        bgmAudioSource = ass[0];
        effectAudioSource = ass[1];
        bgmAudioSource.loop = true; // ���ñ���������ƵԴѭ������
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

    // ����������BGM
    public void PlayMainMenuBGM()
    {
        PlayBgmAudio(������bgm);
    }

    // �����̵����BGM
    public void PlayShopBGM()
    {
        PlayBgmAudio(�̵����bgm);
    }

    // ������Ϸ����BGM
    public void PlayGameBGM()
    {
        PlayBgmAudio(��Ϸ����bgm);
    }

    public void PlayButtonCliclkEffect()
    {
        PlayEffectAudio(��ť���effect);
    }

    public void PlayBuySelectionsEffect()
    {
        PlayEffectAudio(����effect);
    }

    public void PlayRefreshSelectionsEffect()
    {
        PlayEffectAudio(ˢ���̵�effect);
    }
    public void PlayLockSelectionsEffect()
    {
        PlayEffectAudio(������effect);
    }

    public void PlayLevelUpEffect()
    {
        PlayEffectAudio(����effect);
    }

    public void PlayLevelUpWeaponEffect()
    {
        PlayEffectAudio(��������effect);
    }

    public void PlayErrorEffect()
    {
        PlayEffectAudio(����effect);
    }

    // ͨ�ò���BGM����
    private void PlayBgmAudio(AudioClip clip)
    {
        if (bgmAudioSource.clip == clip)
        {
            return; // �����ǰ���ŵ���Ƶ�Ѿ���Ŀ����Ƶ�����ظ�����
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
