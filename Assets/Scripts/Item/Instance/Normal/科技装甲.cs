using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+30������+5������-2
public class �Ƽ�װ�� : BaseItem
{
    public override int ID => 25;

    public override void OnGet()
    {
        base.OnGet();
        attr.������� += 30;
        attr.���� += 5;
        attr.���� -= 2;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
