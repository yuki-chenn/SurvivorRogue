using System;

[Serializable]
// ÿ���ܵ��˺���ս��ʱ�ķ���������1���������30
public class ����֮��֮�� : BaseBuff
{
    public override int ID => 21;

    private float deltaValue = 0;

    public override void OnHurted(DamageInfo info)
    {
        base.OnHurted(info);
        if (deltaValue + float.Epsilon < 30)
        {
            deltaValue += 1;
            GameManager.Instance.Player.attr.���� += 1;
        }
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.Player.attr.���� -= deltaValue;
        deltaValue = 0;
    }

}
