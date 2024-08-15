using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 智力+6，攻击速度+2，暴击率-2%
public class 智慧戒指 : BaseItem
{
    public override int ID => 28;

    public override void OnGet()
    {
        base.OnGet();
        attr.智力 += 6;
        attr.攻击速度 += 2;
        attr.暴击率 -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
