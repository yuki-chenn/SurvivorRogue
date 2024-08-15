// 持续5s受到伤害增加50%
using System;

[Serializable]
public class 变异破甲 : BaseBuff
{
    public override int ID => 4;

    public override void OnBeforeTakeDamage(DamageInfo info)
    {
        base.OnBeforeTakeDamage(info);
        info.bonus += 50;
    }

}
