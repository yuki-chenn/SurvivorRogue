using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 被动 
// 当血量小于等于20%时，造成的所有伤害增加50%，之后每减少5%，造成的所有伤害增加50%
public class 勇者 : Player
{

    public GameObject skillEffect;
    public GameObject passiveEffect;

    private int 背水一战_buff_ID = 1;

    public AudioClip 旋风斩击audio;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPlayerReset()
    {
        base.OnPlayerReset();
        transform.Find("EF_旋风斩击").gameObject.SetActive(false);
        transform.Find("SkillCollider").gameObject.SetActive(false);
        transform.Find("EF_背水一战").gameObject.SetActive(false);
    }

    public override void 使用技能()
    {
        base.使用技能();
        PlayAudioEffect(旋风斩击audio);
        skillEffect.SetActive(true);
    }

    public override void 移动()
    {
        base.移动();
        passiveEffect.transform.localScale = GeometryUtil.GetDirectionScale(passiveEffect.transform.localScale, moveDir, "x");
    }

    public override void TakeDamage(DamageInfo damage)
    {
        base.TakeDamage(damage);
    }

    protected override void OnHPChanged(float oldHp,float newHp)
    {
        base.OnHPChanged(oldHp,newHp);
        // 被动 
        // 当血量小于等于20%时，造成的所有伤害增加50%，之后每减少5%，造成的所有伤害增加50%
        if (newHp <= 0) return;
        var curBuff = buffList.GetBuffById(背水一战_buff_ID);
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
                buffList.AddBuff(背水一战_buff_ID, gameObject, gameObject);
            }
        }

        if (d < 0)
        {
            for (int i = 0; i < -d; ++i)
            {
                buffList.RemoveBuff(背水一战_buff_ID);
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
