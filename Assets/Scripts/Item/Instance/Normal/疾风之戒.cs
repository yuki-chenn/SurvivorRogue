using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 攻击速度+2，力量-3
public class 疾风之戒 : BaseItem
{
    public override int ID => 5;

    public override void OnGet()
    {
        base.OnGet();
        attr.攻击速度 += 2;
        attr.力量 -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
