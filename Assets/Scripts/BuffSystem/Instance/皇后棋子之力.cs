using System;

[Serializable]
// ÿ���˺�����80%��
public class �ʺ�����֮�� : BaseBuff
{
    public override int ID => 8;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 80;
    }

}
