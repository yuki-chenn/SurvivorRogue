using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 最大生命+40，防御+7，闪避率+2，攻击速度-4
public class 深渊龙铠 : BaseItem
{
    public override int ID => 36;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大生命 += 40;
        attr.防御 += 7;
        attr.闪避 += 2;
        attr.攻击速度 -= 4;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
