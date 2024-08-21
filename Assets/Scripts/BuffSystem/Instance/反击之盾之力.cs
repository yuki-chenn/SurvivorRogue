using Survivor.Utils;
using System;

// 每次受到伤害，有50%的概率让攻击者受到200%反弹的固定伤害
[Serializable]
public class 反击之盾之力 : BaseBuff
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
