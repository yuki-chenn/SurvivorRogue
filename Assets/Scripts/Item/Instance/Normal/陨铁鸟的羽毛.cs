using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �ƶ��ٶ�+4������+2%�������˺�-3%
public class ���������ë : BaseItem
{
    public override int ID => 33;

    public override void OnGet()
    {
        base.OnGet();
        attr.�ƶ��ٶ� += 4;
        attr.���� += 2;
        attr.�����˺� -= 3;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
