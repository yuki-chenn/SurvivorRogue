using System;


// ÿ���˺�����10%��
[Serializable]
public class ʿ������֮�� : BaseBuff
{
    public override int ID => 6;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 10;
    }

}
