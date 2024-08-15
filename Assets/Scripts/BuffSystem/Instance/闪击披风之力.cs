using System;

[Serializable]
// ��һ�ι������˫���˺�
public class ��������֮�� : BaseBuff
{
    public override int ID => 25;

    private bool isTrigger = false;

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        if (!isTrigger)
        {
            info.bonus *= 2;
            needRemove = true;
            isTrigger = true;
        }
        
    }

}
