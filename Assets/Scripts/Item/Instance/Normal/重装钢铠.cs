using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+6��������+2%���ƶ��ٶ�-3
public class ��װ���� : BaseItem
{
    public override int ID => 29;

    public override void OnGet()
    {
        base.OnGet();
        attr.���� += 6;
        attr.������ += 2;
        attr.�ƶ��ٶ� -= 3;
        
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
