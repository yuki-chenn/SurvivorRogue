using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ������+2%������-3
public class ���ݶ��� : BaseItem
{
    public override int ID => 13;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 2;
        attr.���� -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
