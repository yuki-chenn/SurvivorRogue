using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ������+3%������+3���������-10
public class ħ���� : BaseItem
{
    public override int ID => 30;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 3;
        attr.���� += 3;
        attr.������� -= 10;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
