using System;


// 被动 
// 每层对蘑菇怪人造成的伤害增加100%
[Serializable]
public class 采蘑菇的兔兔之力 : BaseBuff
{
    public override int ID => 13;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        if(info.target.tag.Equals("Enemy") && info.target.GetComponent<Enemy>().id == 5)
        {
            info.bonus += 100 * curStack;
        }
    }

}
