using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ÖÇÁ¦+4£¬±©»÷ÉËº¦-2%
public class ¹Å·¨Ê¦ÏîÁ´ : BaseItem
{
    public override int ID => 9;

    public override void OnGet()
    {
        base.OnGet();
        attr.ÖÇÁ¦ += 4;
        attr.±©»÷ÉËº¦ -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
