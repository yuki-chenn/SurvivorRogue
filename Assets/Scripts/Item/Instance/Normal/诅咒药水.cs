using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ������+5%�������˺�+7%�������ٶ�+2������-4
public class ����ҩˮ : BaseItem
{
    public override int ID => 39;

    public override void OnGet()
    {
        base.OnGet();
        attr.������ += 5;
        attr.�����˺� += 7;
        attr.�����ٶ� += 2;
        attr.���� -= 4;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
