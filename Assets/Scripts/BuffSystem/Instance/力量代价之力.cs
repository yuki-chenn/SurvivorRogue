using System;

[Serializable]
// �˺�����100%��
public class ��������֮�� : BaseBuff
{
    public override int ID => 23;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 100;
    }

}
