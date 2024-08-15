using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 力量+4，防御-2
public class 兽牙项链 : BaseItem
{
    public override int ID => 7;

    public override void OnGet()
    {
        base.OnGet();
        attr.力量 += 4;
        attr.防御 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
