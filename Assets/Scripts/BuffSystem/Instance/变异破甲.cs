// ����5s�ܵ��˺�����50%
using System;

[Serializable]
public class �����Ƽ� : BaseBuff
{
    public override int ID => 4;

    public override void OnBeforeTakeDamage(DamageInfo info)
    {
        base.OnBeforeTakeDamage(info);
        info.bonus += 50;
    }

}
