using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+3���ƶ��ٶ�-3
public class ���˲� : BaseItem
{
    public override int ID => 22;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 3;
        attr.�ƶ��ٶ� -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
