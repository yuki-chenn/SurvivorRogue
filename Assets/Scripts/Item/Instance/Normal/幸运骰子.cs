using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 幸运+9，暴击率+3，闪避率+1%，收入-7
public class 幸运骰子 : BaseItem
{
    public override int ID => 34;

    public override void OnGet()
    {
        base.OnGet();
        attr.幸运 += 9;
        attr.暴击率 += 3;
        attr.闪避 += 1;
        GameManager.Instance.gameData.salary -= 7;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
