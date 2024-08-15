using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ±©»÷ÉËº¦+5%£¬±©»÷ÂÊ-3%
public class »ÙÃð¾íÖá : BaseItem
{
    public override int ID => 17;

    public override void OnGet()
    {
        base.OnGet();
        attr.±©»÷ÉËº¦ += 5;
        attr.±©»÷ÂÊ -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
