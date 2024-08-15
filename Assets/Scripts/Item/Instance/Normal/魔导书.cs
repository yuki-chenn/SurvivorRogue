using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 闪避率+3%，智力+3，最大生命-10
public class 魔导书 : BaseItem
{
    public override int ID => 30;

    public override void OnGet()
    {
        base.OnGet();
        attr.闪避 += 3;
        attr.智力 += 3;
        attr.最大生命 -= 10;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
