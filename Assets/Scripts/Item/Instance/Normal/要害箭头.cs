using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ������+2%�������˺�-5%
public class Ҫ����ͷ : BaseItem
{
    public override int ID => 15;

    public override void OnGet()
    {
        base.OnGet();
        attr.������ += 2;
        attr.�����˺� -= 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
