using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 暴击率+1%，力量-2，智力-2
public class 精准符文 : BaseItem
{
    public override int ID => 16;

    public override void OnGet()
    {
        base.OnGet();
        attr.暴击率 += 1;
        attr.力量 -= 2;
        attr.智力 -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
