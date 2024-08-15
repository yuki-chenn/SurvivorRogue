using System;


// 被动 
// 2s内抵挡所有攻击的伤害
[Serializable]
public class 防御魔法 : BaseBuff
{
    public override int ID => 26;

    public override void OnBeforeTakeDamage(DamageInfo info)
    {
        base.OnBeforeTakeDamage(info);
        info.bonus = 0;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.OnPlayerReset();
    }

}
