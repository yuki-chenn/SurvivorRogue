using System;


// 每层伤害增加40%，
[Serializable]
public class 主教棋子之力 : BaseBuff
{
    public override int ID => 7;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 40;
    }

}
