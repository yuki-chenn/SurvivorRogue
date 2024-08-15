using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 收入+12，力量+2，智力+2，幸运-8
public class 第一袋金 : BaseItem
{
    public override int ID => 35;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.salary += 12;
        attr.力量 += 2;
        attr.智力 += 2;
        attr.幸运 -= 8;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
