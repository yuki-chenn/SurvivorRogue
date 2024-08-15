using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ·ÀÓù+2£¬¹¥»÷ËÙ¶È-2
public class Ìú¿éÍ·¿ø : BaseItem
{
    public override int ID => 12;

    public override void OnGet()
    {
        base.OnGet();
        attr.·ÀÓù += 2;
        attr.¹¥»÷ËÙ¶È -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
