using System;

[Serializable]
// 每层伤害增加80%，
public class 皇后棋子之力 : BaseBuff
{
    public override int ID => 8;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 80;
    }

}
