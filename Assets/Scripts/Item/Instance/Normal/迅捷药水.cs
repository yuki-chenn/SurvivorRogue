using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �ƶ��ٶ�+2�������ٶ�-2
public class Ѹ��ҩˮ : BaseItem
{
    public override int ID => 20;

    public override void OnGet()
    {
        base.OnGet();
        attr.�ƶ��ٶ� += 2;
        attr.�����ٶ� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
