using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ��������ʱ�����̸���ָ�ȫ��Ѫ���������ü�����ȴ��ֻ�ܴ���һ�Ρ�
public class ����ʥ�� : BaseItem
{
    public override int ID => 50;

    private int ����ʥ��֮��_buff_id = 5;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(����ʥ��֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(����ʥ��֮��_buff_id);
    }




}
