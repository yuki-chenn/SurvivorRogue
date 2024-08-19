using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ħ��ʦ : Player
{
    public GameObject skillGo;
    public GameObject passiveEffect;

    private int ����ħ��_buff_id = 26;

    public AudioClip ����audio;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPlayerReset()
    {
        base.OnPlayerReset();
        passiveEffect.SetActive(false);
    }

    public override void ʹ�ü���()
    {
        base.ʹ�ü���();
        PlayAudioEffect(����audio);
        var skill = Instantiate(skillGo, transform.position, Quaternion.identity, ContainerManager.Instance.weaponObjectContainer);
        skill.transform.Find("SkillCollider").GetComponent<BaseAttackObject>().source = gameObject;
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

    protected override void OnHPChanged(float oldHp, float newHp)
    {
        base.OnHPChanged(oldHp, newHp);
        // ���� 
        // ��Ѫ������20%ʱ������һ������2s�Ļ��ܣ��ֵ����е��˺�
        if(oldHp >= maxHp / 5 && newHp < maxHp / 5)
        {
            buffList.AddBuff(����ħ��_buff_id, gameObject, gameObject);
            passiveEffect.SetActive(true);
        }
    }


}
