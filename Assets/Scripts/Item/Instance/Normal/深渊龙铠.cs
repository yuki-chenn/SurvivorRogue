using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �������+40������+7��������+2�������ٶ�-4
public class ��Ԩ���� : BaseItem
{
    public override int ID => 36;

    public override void OnGet()
    {
        base.OnGet();
        attr.������� += 40;
        attr.���� += 7;
        attr.���� += 2;
        attr.�����ٶ� -= 4;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
