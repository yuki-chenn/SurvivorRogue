
// ���� 
// ��������ʱ�����̸���ָ�ȫ��Ѫ���������ü�����ȴ��
using System;

[Serializable]
public class ����ʥ��֮�� : BaseBuff
{
    public override int ID => 5;

    public override void OnBeforeKilled(DamageInfo info)
    {
        base.OnBeforeKilled(info);
        info.amount = 0;
        owner.GetComponent<BaseCharacter>()?.RestoreAllHp();
        owner.GetComponent<Player>()?.���ü�����ȴ();
        needRemove = true;
    }
}
