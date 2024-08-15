using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 收入+7，幸运+5，暴击伤害+5%，最大生命-50
public class 圣诞礼物 : BaseItem
{
    public override int ID => 40;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.salary += 7;
        attr.幸运 += 5;
        attr.暴击伤害 += 5;
        attr.最大生命 -= 50;

    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
