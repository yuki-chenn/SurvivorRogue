using System;

[Serializable]
// 5s内移动速度增加10%
public class 陨铁鸟之力 : BaseBuff
{
    public override int ID => 10;

    private float deltaValue = 0;

    public override void AddModify()
    {
        clkDuration = Info.Duration;
    }

    public override void OnAdd()
    {
        base.OnAdd();
        if(deltaValue == 0)
        {
            float val = GameManager.Instance.Player.attr.移动速度 * 0.1f * curStack;
            deltaValue = -GameManager.Instance.Player.attr.移动速度;
            GameManager.Instance.Player.attr.移动速度 += val;
            deltaValue += GameManager.Instance.Player.attr.移动速度;
        }
        
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.attr.移动速度 -= deltaValue;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
        deltaValue = 0;
    }

}
