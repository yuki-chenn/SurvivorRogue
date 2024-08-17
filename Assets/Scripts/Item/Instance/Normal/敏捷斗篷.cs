using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ÉÁ±ÜÂÊ+2%£¬·ÀÓù-3
public class Ãô½İ¶·Åñ : BaseItem
{
    public override int ID => 13;

    public override void OnGet()
    {
        base.OnGet();
        attr.ÉÁ±Ü += 2;
        attr.·ÀÓù -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
