using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ÒÆ¶¯ËÙ¶È+4£¬ÉÁ±Ü+2%£¬±©»÷ÉËº¦-3%
public class ÔÉÌúÄñµÄÓğÃ« : BaseItem
{
    public override int ID => 33;

    public override void OnGet()
    {
        base.OnGet();
        attr.ÒÆ¶¯ËÙ¶È += 4;
        attr.ÉÁ±Ü += 2;
        attr.±©»÷ÉËº¦ -= 3;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
