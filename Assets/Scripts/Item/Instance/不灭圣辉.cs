using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 当你死亡时，立刻复活并恢复全部血量，并重置技能冷却，只能触发一次。
public class 不灭圣辉 : BaseItem
{
    public override int ID => 50;

    private int 不灭圣辉之力_buff_id = 5;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(不灭圣辉之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(不灭圣辉之力_buff_id);
    }




}
