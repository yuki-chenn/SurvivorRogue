using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ·ÀÓù+4£¬ĞÒÔË-3
public class ÊØ»¤Ö®¶Ü : BaseItem
{
    public override int ID => 11;

    public override void OnGet()
    {
        base.OnGet();
        attr.·ÀÓù += 4;
        attr.ĞÒÔË -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
