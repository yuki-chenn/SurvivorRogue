using System;

[Serializable]
// ս��ʱÿ��5�룬ս��ʱ�Ĺ����ٶ�����5
public class ʱ�������֮�� : BaseBuff
{
    public override int ID => 19;

    private float deltaValue = 0;

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        // ���ü�ʱ��
        clkTick = Info.TickTime;
        deltaValue = 0;
    }

    public override void OnTick()
    {
        base.OnTick();
        float d = -GameManager.Instance.Player.attr.�����ٶ�;
        GameManager.Instance.Player.attr.�����ٶ� += 5;
        d += GameManager.Instance.Player.attr.�����ٶ�;
        deltaValue += d;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.Player.attr.�����ٶ� -= deltaValue;
        deltaValue = 0;
    }


}
