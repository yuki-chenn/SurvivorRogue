using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// ����+2��ʰȡ��ҵķ�Χ����
public class �����ָ : BaseItem
{
    public override int ID => 42;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.salary += 2;
        GameManager.Instance.gameData.pickDis += 0.5f;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
