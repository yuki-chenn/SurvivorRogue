using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ±©»÷ÉËº¦+2%£¬ÖÇÁ¦-3
public class ÊÈÑªÖ®½£ : BaseItem
{
    public override int ID => 18;

    public override void OnGet()
    {
        base.OnGet();
        attr.±©»÷ÉËº¦ += 2;
        attr.ÖÇÁ¦ -= 3;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
