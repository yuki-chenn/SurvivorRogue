using Survivor.Utils;
using System;
using UnityEngine;

// ���� 
// ÿ����10%�ĸ�������1ö���
[Serializable]
public class ��һ���֮�� : BaseBuff
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
