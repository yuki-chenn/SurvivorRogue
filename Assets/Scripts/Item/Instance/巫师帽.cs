using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����ʱ��10%�ĸ�������һö���
public class ��ʦñ : BaseItem
{
    public override int ID => 61;

    private int ��ʦñ֮��_buff_id = 11;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(��ʦñ֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(��ʦñ֮��_buff_id);
    }




}
