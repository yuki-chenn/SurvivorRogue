using System;


// ���� 
// ÿ��buff����ɵ������˺�����50%
[Serializable]
public class ��ˮһս : BaseBuff
{
    public override int ID => 1;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 50;
    }

}
