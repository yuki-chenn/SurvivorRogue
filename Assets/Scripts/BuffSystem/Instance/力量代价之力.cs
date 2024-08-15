using System;

[Serializable]
// 伤害增加100%，
public class 力量代价之力 : BaseBuff
{
    public override int ID => 23;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 100;
    }

}
