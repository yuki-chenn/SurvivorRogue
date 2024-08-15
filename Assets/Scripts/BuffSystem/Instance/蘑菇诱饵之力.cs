using Survivor.Template;
using System;

[Serializable]
// 战斗开始时，在随机位置召唤一个蘑菇怪人。
public class 蘑菇诱饵之力 : BaseBuff
{
    public override int ID => 14;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        WaveTplInfo 蘑菇怪人Tpl = new WaveTplInfo();
        蘑菇怪人Tpl.EnemyID = 5;
        蘑菇怪人Tpl.RandomPosition = 1;
        蘑菇怪人Tpl.Count = 1;
        蘑菇怪人Tpl.RecycleCount = curStack;
        GameManager.Instance.GenerateEnemy(蘑菇怪人Tpl);
    }

}
