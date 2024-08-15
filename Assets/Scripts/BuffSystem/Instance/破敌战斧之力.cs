using System;

[Serializable]
// 5s内伤害增加4%
public class 破敌战斧之力 : BaseBuff
{
    public override int ID => 9;

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 4;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
    }

}
