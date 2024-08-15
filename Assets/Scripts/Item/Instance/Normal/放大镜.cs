using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 暴击率+3%，暴击伤害+5%，力量-2
public class 放大镜 : BaseItem
{
    public override int ID => 31;

    public override void OnGet()
    {
        base.OnGet();
        attr.暴击率 += 3;
        attr.暴击伤害 += 5;
        attr.力量 -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
