using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 对蘑菇怪人造成的伤害增加100%
public class 采蘑菇的兔兔 : BaseItem
{
    public override int ID => 65;

    private int 采蘑菇的兔兔之力_buff_id = 13;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(采蘑菇的兔兔之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(采蘑菇的兔兔之力_buff_id);
    }




}
