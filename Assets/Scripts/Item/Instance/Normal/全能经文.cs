using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 力量+10，智力+10，暴击率+2%，防御-8
public class 全能经文 : BaseItem
{
    public override int ID => 37;

    public override void OnGet()
    {
        base.OnGet();
        attr.力量 += 10;
        attr.智力 += 10;
        attr.暴击率 += 2;
        attr.防御 -= 8;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
