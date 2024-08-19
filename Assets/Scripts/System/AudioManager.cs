using Survivor.Base;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public float effectScale = 0.6f;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true; // ���ñ���������ƵԴѭ������
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
        if (audioSource.clip == clip)
        {
            return; // �����ǰ���ŵ���Ƶ�Ѿ���Ŀ����Ƶ�����ظ�����
        }

        audioSource.clip = clip;
        audioSource.Play();
    }

    private void PlayEffectAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip, effectScale);
    }
}
