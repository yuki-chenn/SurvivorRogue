using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �������+40�������¸����ο�ʼʱ������ֵֻ��20%
public class Ѫ֮��Լ : BaseItem
{
    public override int ID => 2;

    private int Ѫ֮��Լ֮��_buff_id = 3;

    public override void OnGet()
    {
        base.OnGet();
        attr.������� += 40;
        GameManager.Instance.Player.buffList.AddBuff(Ѫ֮��Լ֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
