using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 最大生命+20，攻击速度+3，力量+5，智力+5，防御+3，闪避率+2%，暴击率+2%，暴击伤害+4%，移动速度+3，幸运+4
public class 自然之力 : BaseItem
{
    public override int ID => 41;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大生命 += 20;
        attr.攻击速度 += 3;
        attr.力量 += 5;
        attr.智力 += 5;
        attr.防御 += 3;
        attr.闪避 += 2;
        attr.暴击率 += 2;
        attr.暴击伤害 += 4;
        attr.移动速度 += 3;
        attr.幸运 += 4;

    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
