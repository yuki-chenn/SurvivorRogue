using System;

[Serializable]
// 受到伤害增加50%
public class 力量代价之咒 : BaseBuff
{
    public override int ID => 24;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeTakeDamage(DamageInfo info)
    {
        base.OnBeforeTakeDamage(info);
        info.bonus += 50 * curStack;
    }

}
