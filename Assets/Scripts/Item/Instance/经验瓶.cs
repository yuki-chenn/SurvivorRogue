using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �������1000�㾭��ֵ
public class ����ƿ : BaseItem
{
    public override int ID => 56;

    private int expValue
    {
        get
        {
            return 500;
        }
    }

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.GainExp(expValue);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
