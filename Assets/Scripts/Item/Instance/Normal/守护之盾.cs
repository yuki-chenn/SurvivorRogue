using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+4������-3
public class �ػ�֮�� : BaseItem
{
    public override int ID => 11;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 4;
        attr.���� -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
