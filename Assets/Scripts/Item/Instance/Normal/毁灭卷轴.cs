using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �����˺�+5%��������-3%
public class ������� : BaseItem
{
    public override int ID => 17;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����˺� += 5;
        attr.������ -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
