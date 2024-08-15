using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 幸运+3，移动速度-3
public class 幸运草 : BaseItem
{
    public override int ID => 22;

    public override void OnGet()
    {
        base.OnGet();
        attr.幸运 += 3;
        attr.移动速度 -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
