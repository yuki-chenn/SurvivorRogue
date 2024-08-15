using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 闪避率+1%，攻击速度-2
public class 魔法披风 : BaseItem
{
    public override int ID => 14;

    public override void OnGet()
    {
        base.OnGet();
        attr.闪避 += 1;
        attr.攻击速度 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
