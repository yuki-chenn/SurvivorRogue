using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �������+40�������¸����ο�ʼʱ������ֵֻ��20%
public class ��յ�� : BaseItem
{
    public override int ID => 64;

    private int ��յ��֮��_buff_id = 12;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.Player.buffList.AddBuff(��յ��֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        GameManager.Instance.Player.buffList.RemoveBuff(��յ��֮��_buff_id);
    }




}
