using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �����˺�+8%������+3�������ٶ�-2
public class ���� : BaseItem
{
    public override int ID => 32;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����˺� += 8;
        attr.���� += 3;
        attr.�����ٶ� -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
