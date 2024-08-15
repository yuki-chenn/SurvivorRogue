using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 收入+7，幸运-5
public class 诅咒金币 : BaseItem
{
    public override int ID => 23;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.salary += 7;
        attr.幸运 -= 5;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
