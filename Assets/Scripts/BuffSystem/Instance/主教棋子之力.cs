using System;


// ÿ���˺�����40%��
[Serializable]
public class ��������֮�� : BaseBuff
{
    public override int ID => 7;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 40;
    }

}
