using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 攻击速度+1，暴击率-1%
public class 迅捷之刃 : BaseItem
{
    public override int ID => 6;

    public override void OnGet()
    {
        base.OnGet();
        attr.攻击速度 += 1;
        attr.暴击率 -= 1;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
