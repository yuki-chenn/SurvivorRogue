using System;

[Serializable]
// 每次受到伤害，战斗时的防御力增加1，最多增加30
public class 钢铁之心之力 : BaseBuff
{
    public override int ID => 21;

    private float deltaValue = 0;

    public override void OnHurted(DamageInfo info)
    {
        base.OnHurted(info);
        if (deltaValue + float.Epsilon < 30)
        {
            deltaValue += 1;
            GameManager.Instance.Player.attr.防御 += 1;
        }
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.Player.attr.防御 -= deltaValue;
        deltaValue = 0;
    }

}
