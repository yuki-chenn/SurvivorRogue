using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ������+1%������-2������-2
public class ��׼���� : BaseItem
{
    public override int ID => 16;

    public override void OnGet()
    {
        base.OnGet();
        attr.������ += 1;
        attr.���� -= 2;
        attr.���� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
