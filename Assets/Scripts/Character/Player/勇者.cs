using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� 
// ��Ѫ��С�ڵ���20%ʱ����ɵ������˺�����50%��֮��ÿ����5%����ɵ������˺�����50%
public class ���� : Player
{

    public GameObject skillEffect;
    public GameObject passiveEffect;

    private int ��ˮһս_buff_ID = 1;

    public AudioClip ����ն��audio;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPlayerReset()
    {
        base.OnPlayerReset();
        transform.Find("EF_����ն��").gameObject.SetActive(false);
        transform.Find("SkillCollider").gameObject.SetActive(false);
        transform.Find("EF_��ˮһս").gameObject.SetActive(false);
    }

    public override void ʹ�ü���()
    {
        base.ʹ�ü���();
        PlayAudioEffect(����ն��audio);
        skillEffect.SetActive(true);
    }

    public override void �ƶ�()
    {
        base.�ƶ�();
        passiveEffect.transform.localScale = GeometryUtil.GetDirectionScale(passiveEffect.transform.localScale, moveDir, "x");
    }

    public override void TakeDamage(DamageInfo damage)
    {
        base.TakeDamage(damage);
    }

    protected override void OnHPChanged(float oldHp,float newHp)
    {
        base.OnHPChanged(oldHp,newHp);
        // ���� 
        // ��Ѫ��С�ڵ���20%ʱ����ɵ������˺�����50%��֮��ÿ����5%����ɵ������˺�����50%
        if (newHp <= 0) return;
        var curBuff = buffList.GetBuffById(��ˮһս_buff_ID);
        int curStack = curBuff == null ? 0 : curBuff.curStack;
        float curPercent = newHp / maxHp;
        int newStack = 0;
        if (curPercent <= 0.2f)
        {
            newStack = Mathf.FloorToInt((0.2f - curPercent) / 0.05f) + 1;
        }

        int d = newStack - curStack;


        if (d > 0)
        {
            for (int i = 0; i < d; ++i)
            {
                buffList.AddBuff(��ˮһս_buff_ID, gameObject, gameObject);
            }
        }

        if (d < 0)
        {
            for (int i = 0; i < -d; ++i)
            {
                buffList.RemoveBuff(��ˮһս_buff_ID);
            }
        }

        UpdatePassiveEffect(curStack, newStack);
    }

    private void UpdatePassiveEffect(int curStack, int newStack)
    {
        if (curStack == newStack) return;

        if(newStack == 0)
        {
            passiveEffect.SetActive(false);
            passiveEffect.GetComponent<ParticleSystem>().Stop();
            passiveEffect.GetComponent<ParticleSystem>().Clear();
        }
        else
        {
            passiveEffect.GetComponent<ParticleSystem>().Stop();
            passiveEffect.GetComponent<ParticleSystem>().Clear();
            for (int i = 2; i < passiveEffect.transform.childCount; ++i)
            {
                passiveEffect.transform.GetChild(i).gameObject.SetActive(i - 2 < newStack);
            }
            passiveEffect.SetActive(true);
            passiveEffect.GetComponent<ParticleSystem>().Play();
        }
    }
}
