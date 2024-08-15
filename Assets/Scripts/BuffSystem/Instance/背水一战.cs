using System;


// 被动 
// 每层buff让造成的所有伤害增加50%
[Serializable]
public class 背水一战 : BaseBuff
{
    public override int ID => 1;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 50;
    }

}
