using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ÿӵ��20��ң�ս��ʱ��ɵ��˺�����2.5%
public class ��ƻ�� : BaseItem
{
    public override int ID => 81;

    private int ��ƻ��֮��_buff_id = 17;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(��ƻ��֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(��ƻ��֮��_buff_id);
    }




}
