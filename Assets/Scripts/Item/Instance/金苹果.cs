using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 每拥有20金币，战斗时造成的伤害增加2.5%
public class 金苹果 : BaseItem
{
    public override int ID => 81;

    private int 金苹果之力_buff_id = 17;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(金苹果之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(金苹果之力_buff_id);
    }




}
