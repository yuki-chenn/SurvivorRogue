using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 移动速度+2，攻击速度-2
public class 迅捷药水 : BaseItem
{
    public override int ID => 20;

    public override void OnGet()
    {
        base.OnGet();
        attr.移动速度 += 2;
        attr.攻击速度 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
