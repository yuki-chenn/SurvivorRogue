using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 最大生命+20，移动速度-2
public class 生命铠甲 : BaseItem
{
    public override int ID => 4;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大生命 += 20;
        attr.移动速度 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
