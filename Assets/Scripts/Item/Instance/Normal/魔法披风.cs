using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ������+1%�������ٶ�-2
public class ħ������ : BaseItem
{
    public override int ID => 14;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 1;
        attr.�����ٶ� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
