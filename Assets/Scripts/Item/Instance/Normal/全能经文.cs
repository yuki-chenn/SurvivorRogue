using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+10������+10��������+2%������-8
public class ȫ�ܾ��� : BaseItem
{
    public override int ID => 37;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 10;
        attr.���� += 10;
        attr.������ += 2;
        attr.���� -= 8;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
