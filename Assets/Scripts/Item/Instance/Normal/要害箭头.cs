using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ±©»÷ÂÊ+2%£¬±©»÷ÉËº¦-5%
public class Òªº¦¼ýÍ· : BaseItem
{
    public override int ID => 15;

    public override void OnGet()
    {
        base.OnGet();
        attr.±©»÷ÂÊ += 2;
        attr.±©»÷ÉËº¦ -= 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
