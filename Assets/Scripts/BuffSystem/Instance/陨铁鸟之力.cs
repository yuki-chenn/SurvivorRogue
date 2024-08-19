using System;

[Serializable]
// 5s���ƶ��ٶ�����10%
public class ������֮�� : BaseBuff
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
            float val = GameManager.Instance.Player.attr.�ƶ��ٶ� * 0.1f * curStack;
            deltaValue = -GameManager.Instance.Player.attr.�ƶ��ٶ�;
            GameManager.Instance.Player.attr.�ƶ��ٶ� += val;
            deltaValue += GameManager.Instance.Player.attr.�ƶ��ٶ�;
        }
        
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.attr.�ƶ��ٶ� -= deltaValue;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
        deltaValue = 0;
    }

}
