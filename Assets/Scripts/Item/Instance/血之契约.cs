using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 最大生命+40，但是下个波次开始时，生命值只有20%
public class 血之契约 : BaseItem
{
    public override int ID => 2;

    private int 血之契约之咒_buff_id = 3;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大生命 += 40;
        GameManager.Instance.Player.buffList.AddBuff(血之契约之咒_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
