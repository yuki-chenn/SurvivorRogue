using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 吸取所有的掉落物品。
public class 收集之握 : BaseItem
{
    public override int ID => 43;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.pickDis = 25;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
