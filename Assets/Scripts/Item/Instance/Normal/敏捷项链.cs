using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �����ٶ�+4���ƶ��ٶ�+3������-2
public class �������� : BaseItem
{
    public override int ID => 26;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����ٶ� += 4;
        attr.�ƶ��ٶ� += 3;
        attr.���� -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
