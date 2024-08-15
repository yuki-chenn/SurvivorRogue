using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ±©»÷ÂÊ+5%£¬±©»÷ÉËº¦+7%£¬¹¥»÷ËÙ¶È+2£¬ĞÒÔË-4
public class ×çÖäÒ©Ë® : BaseItem
{
    public override int ID => 39;

    public override void OnGet()
    {
        base.OnGet();
        attr.±©»÷ÂÊ += 5;
        attr.±©»÷ÉËº¦ += 7;
        attr.¹¥»÷ËÙ¶È += 2;
        attr.ĞÒÔË -= 4;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
