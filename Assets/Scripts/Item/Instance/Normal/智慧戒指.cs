using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+6�������ٶ�+2��������-2%
public class �ǻ۽�ָ : BaseItem
{
    public override int ID => 28;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 6;
        attr.�����ٶ� += 2;
        attr.������ -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
