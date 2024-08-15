using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 攻击速度+4，移动速度+3，防御-2
public class 敏捷项链 : BaseItem
{
    public override int ID => 26;

    public override void OnGet()
    {
        base.OnGet();
        attr.攻击速度 += 4;
        attr.移动速度 += 3;
        attr.防御 -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
