
using System;


// 每次击败一个敌人时，恢复5点生命值
[Serializable]
public class 嗜血剑之力 : BaseBuff
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
