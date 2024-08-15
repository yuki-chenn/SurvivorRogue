using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 力量+6，最大生命+10，闪避率-2%
public class 力量宝石 : BaseItem
{
    public override int ID => 27;

    public override void OnGet()
    {
        base.OnGet();
        attr.力量 += 6;
        attr.最大生命 += 10;
        attr.闪避 -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
