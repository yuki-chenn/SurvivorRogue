using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 攻击速度+3，移动速度+3，最大生命+10，暴击率-3%
public class 追风符文 : BaseItem
{
    public override int ID => 38;

    public override void OnGet()
    {
        base.OnGet();
        attr.攻击速度 += 3;
        attr.移动速度 += 3;
        attr.最大生命 += 10;
        attr.暴击率 -= 3;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
