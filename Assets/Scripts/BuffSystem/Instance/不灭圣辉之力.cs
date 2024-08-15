
// 被动 
// 当你死亡时，立刻复活并恢复全部血量，并重置技能冷却。
using System;

[Serializable]
public class 不灭圣辉之力 : BaseBuff
{
    public override int ID => 5;

    public override void OnBeforeKilled(DamageInfo info)
    {
        base.OnBeforeKilled(info);
        info.amount = 0;
        owner.GetComponent<BaseCharacter>()?.RestoreAllHp();
        owner.GetComponent<Player>()?.重置技能冷却();
        needRemove = true;
    }
}
