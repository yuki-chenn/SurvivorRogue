using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+9��������+3��������+1%������-7
public class �������� : BaseItem
{
    public override int ID => 34;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 9;
        attr.������ += 3;
        attr.���� += 1;
        GameManager.Instance.gameData.salary -= 7;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
