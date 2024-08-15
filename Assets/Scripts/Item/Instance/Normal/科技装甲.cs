using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 生命+30，防御+5，智力-2
public class 科技装甲 : BaseItem
{
    public override int ID => 25;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大生命 += 30;
        attr.防御 += 5;
        attr.智力 -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
