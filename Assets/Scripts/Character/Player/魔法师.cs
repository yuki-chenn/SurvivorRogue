using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class 魔法师 : Player
{
    public GameObject skillGo;
    public GameObject passiveEffect;

    private int 防御魔法_buff_id = 26;

    public AudioClip 星落audio;

    protected override void Start()
    {
        base.Start();
    }

    public override void OnPlayerReset()
    {
        base.OnPlayerReset();
        passiveEffect.SetActive(false);
    }

    public override void 使用技能()
    {
        base.使用技能();
        PlayAudioEffect(星落audio);
        var skill = Instantiate(skillGo, transform.position, Quaternion.identity, ContainerManager.Instance.weaponObjectContainer);
        skill.transform.Find("SkillCollider").GetComponent<BaseAttackObject>().source = gameObject;
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

    protected override void OnHPChanged(float oldHp, float newHp)
    {
        base.OnHPChanged(oldHp, newHp);
        // 被动 
        // 当血量低于20%时，生成一个持续2s的护盾，抵挡所有的伤害
        if(oldHp >= maxHp / 5 && newHp < maxHp / 5)
        {
            buffList.AddBuff(防御魔法_buff_id, gameObject, gameObject);
            passiveEffect.SetActive(true);
        }
    }


}
