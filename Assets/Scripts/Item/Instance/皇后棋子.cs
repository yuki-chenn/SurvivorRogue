using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ��һ��ս���˺�����80%����ս���������Զ����۸õ���
public class �ʺ����� : BaseItem
{
    public override int ID => 58;

    private int �ʺ�����֮��_buff_id = 8;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(�ʺ�����֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(�ʺ�����֮��_buff_id);
    }




}
