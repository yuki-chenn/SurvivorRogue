using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �������+20�������ٶ�+3������+5������+5������+3��������+2%��������+2%�������˺�+4%���ƶ��ٶ�+3������+4
public class ��Ȼ֮�� : BaseItem
{
    public override int ID => 41;

    public override void OnGet()
    {
        base.OnGet();
        attr.������� += 20;
        attr.�����ٶ� += 3;
        attr.���� += 5;
        attr.���� += 5;
        attr.���� += 3;
        attr.���� += 2;
        attr.������ += 2;
        attr.�����˺� += 4;
        attr.�ƶ��ٶ� += 3;
        attr.���� += 4;

    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
