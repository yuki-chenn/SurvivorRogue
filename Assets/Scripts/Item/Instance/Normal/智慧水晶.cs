using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 智力+3，力量-2
public class 智慧水晶 : BaseItem
{
    public override int ID => 10;

    public override void OnGet()
    {
        base.OnGet();
        attr.智力 += 3;
        attr.力量 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
