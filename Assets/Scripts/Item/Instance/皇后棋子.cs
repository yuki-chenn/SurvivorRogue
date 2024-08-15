using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 下一场战斗伤害增加80%，在战斗结束后自动出售该道具
public class 皇后棋子 : BaseItem
{
    public override int ID => 58;

    private int 皇后棋子之力_buff_id = 8;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(皇后棋子之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(皇后棋子之力_buff_id);
    }




}
