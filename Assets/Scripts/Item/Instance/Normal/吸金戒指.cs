using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// 收入+2，拾取金币的范围增大。
public class 吸金戒指 : BaseItem
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
