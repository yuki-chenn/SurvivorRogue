using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �����ٶ�+3���ƶ��ٶ�+3���������+10��������-3%
public class ׷����� : BaseItem
{
    public override int ID => 38;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����ٶ� += 3;
        attr.�ƶ��ٶ� += 3;
        attr.������� += 10;
        attr.������ -= 3;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
