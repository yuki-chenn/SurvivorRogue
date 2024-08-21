using Survivor.Utils;
using System;

// ÿ���ܵ��˺�����50%�ĸ����ù������ܵ�200%�����Ĺ̶��˺�
[Serializable]
public class ����֮��֮�� : BaseBuff
{
    public override int ID => 22;

    private float probability => curStack * 0.5f;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnHurted(DamageInfo info)
    {
        base.OnHurted(info);
        if (RandomUtil.IsProbabilityMet(probability) && info.source != null)
        {
            float dmg = info.Value * 2f;
            DamageInfo damage = new DamageInfo(info.target, info.source, dmg);
            info.source?.GetComponent<Enemy>()?.TakeDamage(damage);
        }
    }
}
