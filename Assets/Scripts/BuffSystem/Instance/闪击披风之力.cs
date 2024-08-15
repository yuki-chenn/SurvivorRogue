using System;

[Serializable]
// 下一次攻击造成双倍伤害
public class 闪击披风之力 : BaseBuff
{
    public override int ID => 25;

    private bool isTrigger = false;

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        if (!isTrigger)
        {
            info.bonus *= 2;
            needRemove = true;
            isTrigger = true;
        }
        
    }

}
