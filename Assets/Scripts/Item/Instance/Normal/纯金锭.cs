using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 收入+5，最大生命-20
public class 纯金锭 : BaseItem
{
    public override int ID => 24;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.salary += 5;
        attr.最大生命 -= 20;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
