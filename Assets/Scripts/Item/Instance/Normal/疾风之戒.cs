using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �����ٶ�+2������-3
public class ����֮�� : BaseItem
{
    public override int ID => 5;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����ٶ� += 2;
        attr.���� -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
