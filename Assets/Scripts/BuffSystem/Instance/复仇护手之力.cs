using System;

[Serializable]
// 每次受到攻击后，增加2%的暴击率，最多增加20%，持续5s。
public class 复仇护手之力 : BaseBuff
{
    public override int ID => 15;

    public override void AddModify()
    {
        AddStack();
        clkDuration = Info.Duration;
    }

    private float deltaValue = 0;

    public override void OnAdd()
    {
        base.OnAdd();
        if(deltaValue < 20)
        {
            float d = -GameManager.Instance.Player.attr.暴击率;
            GameManager.Instance.Player.attr.暴击率 += 2;
            d += GameManager.Instance.Player.attr.暴击率;
            deltaValue += d;
        }
        
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.attr.暴击率 -= deltaValue;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
    }

}
