using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ��������ʺͱ������������Ϊ80%������+10%������+10%
public class �������� : BaseItem
{
    public override int ID => 49;

    public override void OnGet()
    {
        base.OnGet();
        attr.��󱩻��� = 80;
        attr.��������� = 80;
        attr.������ += 10;
        attr.���� += 10;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
