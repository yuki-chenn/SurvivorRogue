using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 暴击伤害+8%，力量+3，攻击速度-2
public class 冰砾 : BaseItem
{
    public override int ID => 32;

    public override void OnGet()
    {
        base.OnGet();
        attr.暴击伤害 += 8;
        attr.力量 += 3;
        attr.攻击速度 -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
