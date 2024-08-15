using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 力量+3，智力-2
public class 力量护腕 : BaseItem
{
    public override int ID => 8;

    public override void OnGet()
    {
        base.OnGet();
        attr.力量 += 3;
        attr.智力 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
