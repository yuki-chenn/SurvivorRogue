using Survivor.Utils;
using System;
using UnityEngine;


// ���� 
// ����ʱ��10%�ĸ�������һö���
[Serializable]
public class ��ʦñ֮�� : BaseBuff
{
    public override int ID => 11;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnCritical()
    {
        base.OnCritical();
        for (int i = 0; i < curStack; ++i)
        {
            if (!RandomUtil.IsProbabilityMet(0.1f)) continue;
            var pos = GameManager.Instance.GetPlayerPosition();
            if (pos != null)
            {
                GameManager.Instance.GenerateDrop(DropEnum.Money, (Vector3)pos);
            }

        }
    }

}
