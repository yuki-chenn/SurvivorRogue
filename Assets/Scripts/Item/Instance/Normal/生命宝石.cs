using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �������+30��������-2%
public class ������ʯ : BaseItem
{
    public override int ID => 3;

    public override void OnGet()
    {
        base.OnGet();
        attr.������� += 30;
        attr.���� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
