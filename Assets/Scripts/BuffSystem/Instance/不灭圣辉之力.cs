
// 被动 
// 当你死亡时，立刻复活并恢复全部血量，并重置技能冷却。生成一个持续3秒的光环并抵挡所有飞行道具，并对周围的敌人造成伤害
using System;

[Serializable]
public class 不灭圣辉之力 : BaseBuff
{
    public override int ID => 5;

    bool isTrigger = false;

    public override void OnBeforeKilled(DamageInfo info)
    {
        base.OnBeforeKilled(info);
        if (!isTrigger)
        {
            isTrigger = true;
            info.amount = 0;
            GameManager.Instance.Player.RestoreAllHp();
            GameManager.Instance.Player.重置技能冷却();
            GameManager.Instance.生成不灭圣辉();
            needRemove = true;
        }
        
    }
}
