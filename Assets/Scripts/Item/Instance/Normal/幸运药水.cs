using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+7������-5
public class ����ҩˮ : BaseItem
{
    public override int ID => 21;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 7;
        GameManager.Instance.gameData.salary -= 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
