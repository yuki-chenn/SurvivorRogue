using System;

[Serializable]
// �ܵ��˺�����50%
public class ��������֮�� : BaseBuff
{
    public override int ID => 24;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeTakeDamage(DamageInfo info)
    {
        base.OnBeforeTakeDamage(info);
        info.bonus += 50 * curStack;
    }

}
