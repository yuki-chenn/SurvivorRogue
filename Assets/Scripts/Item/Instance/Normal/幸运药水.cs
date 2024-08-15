using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 幸运+7，收入-5
public class 幸运药水 : BaseItem
{
    public override int ID => 21;

    public override void OnGet()
    {
        base.OnGet();
        attr.幸运 += 7;
        GameManager.Instance.gameData.salary -= 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
