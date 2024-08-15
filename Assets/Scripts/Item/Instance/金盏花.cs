using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 最大生命+40，但是下个波次开始时，生命值只有20%
public class 金盏花 : BaseItem
{
    public override int ID => 64;

    private int 金盏花之力_buff_id = 12;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.Player.buffList.AddBuff(金盏花之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        GameManager.Instance.Player.buffList.RemoveBuff(金盏花之力_buff_id);
    }




}
