using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// �������+20���ƶ��ٶ�-2
public class �������� : BaseItem
{
    public override int ID => 4;

    public override void OnGet()
    {
        base.OnGet();
        attr.������� += 20;
        attr.�ƶ��ٶ� -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
