using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ��Ģ��������ɵ��˺�����100%
public class ��Ģ�������� : BaseItem
{
    public override int ID => 65;

    private int ��Ģ��������֮��_buff_id = 13;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(��Ģ��������֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(��Ģ��������֮��_buff_id);
    }




}
