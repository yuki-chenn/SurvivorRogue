
// ���� 
// ��������ʱ�����̸���ָ�ȫ��Ѫ���������ü�����ȴ������һ������3��Ĺ⻷���ֵ����з��е��ߣ�������Χ�ĵ�������˺�
using System;

[Serializable]
public class ����ʥ��֮�� : BaseBuff
{
    public override int ID => 5;

    bool isTrigger = false;

    public override void OnBeforeKilled(DamageInfo info)
    {
        base.OnBeforeKilled(info);
        if (!isTrigger)
        {
            isTrigger = true;
            info.amount = 0;
            GameManager.Instance.Player.RestoreAllHp();
            GameManager.Instance.Player.���ü�����ȴ();
            GameManager.Instance.���ɲ���ʥ��();
            needRemove = true;
        }
        
    }
}
