using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 防御+6，暴击率+2%，移动速度-3
public class 重装钢铠 : BaseItem
{
    public override int ID => 29;

    public override void OnGet()
    {
        base.OnGet();
        attr.防御 += 6;
        attr.暴击率 += 2;
        attr.移动速度 -= 3;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
