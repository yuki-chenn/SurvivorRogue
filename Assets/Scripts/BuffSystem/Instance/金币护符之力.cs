using Survivor.Utils;
using System;
using UnityEngine;

// 被动 
// 每层有10%的概率生成1枚金币
[Serializable]
public class 金币护符之力 : BaseBuff
{
    public override int ID => 2;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnHurted(DamageInfo info)
    {
        base.OnHurted(info);
        for (int i = 0; i < curStack; ++i)
        {
            if (!RandomUtil.IsProbabilityMet(0.1f)) continue;
            var pos = GameManager.Instance.GetPlayerPosition();
            if(pos != null)
            {
                GameManager.Instance.GenerateDrop(DropEnum.Money, (Vector3)pos);
            }
            
        }
    }

}
