using System;


// ���� 
// ÿ���Ģ��������ɵ��˺�����100%
[Serializable]
public class ��Ģ��������֮�� : BaseBuff
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
