using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 移动速度+3，最大生命-20
public class 疾步之靴 : BaseItem
{
    public override int ID => 19;

    public override void OnGet()
    {
        base.OnGet();
        attr.移动速度 += 3;
        attr.最大生命 -= 20;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
