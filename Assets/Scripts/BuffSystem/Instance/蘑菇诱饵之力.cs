using Survivor.Template;
using System;

[Serializable]
// ս����ʼʱ�������λ���ٻ�һ��Ģ�����ˡ�
public class Ģ���ն�֮�� : BaseBuff
{
    public override int ID => 14;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        WaveTplInfo Ģ������Tpl = new WaveTplInfo();
        Ģ������Tpl.EnemyID = 5;
        Ģ������Tpl.RandomPosition = 1;
        Ģ������Tpl.Count = 1;
        Ģ������Tpl.RecycleCount = curStack;
        GameManager.Instance.GenerateEnemy(Ģ������Tpl);
    }

}
