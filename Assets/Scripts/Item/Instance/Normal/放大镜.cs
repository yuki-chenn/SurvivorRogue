using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ������+3%�������˺�+5%������-2
public class �Ŵ� : BaseItem
{
    public override int ID => 31;

    public override void OnGet()
    {
        base.OnGet();
        attr.������ += 3;
        attr.�����˺� += 5;
        attr.���� -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
