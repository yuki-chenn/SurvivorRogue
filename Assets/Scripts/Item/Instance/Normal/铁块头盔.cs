using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+2�������ٶ�-2
public class ����ͷ�� : BaseItem
{
    public override int ID => 12;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 2;
        attr.�����ٶ� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
