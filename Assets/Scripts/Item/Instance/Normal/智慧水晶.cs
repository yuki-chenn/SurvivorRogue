using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+3������-2
public class �ǻ�ˮ�� : BaseItem
{
    public override int ID => 10;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 3;
        attr.���� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
