using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+4������-2
public class �������� : BaseItem
{
    public override int ID => 7;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 4;
        attr.���� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
