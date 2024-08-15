using System;


// 每拥有20金币，战斗时造成的伤害增加2.5%
[Serializable]
public class 金苹果之力 : BaseBuff
{
    public override int ID => 17;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        int times = GameManager.Instance.gameData.money / 20;
        info.bonus += curStack * 2.5f * times;
    }

}
