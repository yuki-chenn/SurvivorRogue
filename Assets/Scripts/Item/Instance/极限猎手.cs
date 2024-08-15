using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 你的闪避率和暴击率上限提高为80%，闪避+10%，暴击+10%
public class 极限猎手 : BaseItem
{
    public override int ID => 49;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大暴击率 = 80;
        attr.最大闪避率 = 80;
        attr.暴击率 += 10;
        attr.闪避 += 10;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
