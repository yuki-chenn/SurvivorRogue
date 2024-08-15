using System;


// 每层伤害增加10%，
[Serializable]
public class 士兵棋子之力 : BaseBuff
{
    public override int ID => 6;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 10;
    }

}
