using System;

[Serializable]
// 5s���˺�����4%
public class �Ƶ�ս��֮�� : BaseBuff
{
    public override int ID => 9;

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        info.bonus += curStack * 4;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
    }

}
