using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+12������+2������+2������-8
public class ��һ���� : BaseItem
{
    public override int ID => 35;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.salary += 12;
        attr.���� += 2;
        attr.���� += 2;
        attr.���� -= 8;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
