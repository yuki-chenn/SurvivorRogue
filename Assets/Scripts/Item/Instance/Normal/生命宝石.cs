using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 最大生命+30，闪避率-2%
public class 生命宝石 : BaseItem
{
    public override int ID => 3;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大生命 += 30;
        attr.闪避 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
