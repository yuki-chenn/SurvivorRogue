using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 暴击时有10%的概率生成一枚金币
public class 巫师帽 : BaseItem
{
    public override int ID => 61;

    private int 巫师帽之力_buff_id = 11;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(巫师帽之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(巫师帽之力_buff_id);
    }




}
