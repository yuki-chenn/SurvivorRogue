
using System;


// ÿ�λ���һ������ʱ���ָ�5������ֵ
[Serializable]
public class ��Ѫ��֮�� : BaseBuff
{
    public override int ID => 16;

    private float value = 5f;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnKill()
    {
        base.OnKill();
        GameManager.Instance.RestoreHP(value * curStack);
    }

}
