using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �����ٶ�+1��������-1%
public class Ѹ��֮�� : BaseItem
{
    public override int ID => 6;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����ٶ� += 1;
        attr.������ -= 1;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
