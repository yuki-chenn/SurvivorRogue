
// ÿӵ��20�����ˣ�ս��ʱ�ı���������1%������������1%���������15%���Ҳ��ᳬ�������ʺ�����������
using System;

[Serializable]
public class ���˻���֮�� : BaseBuff
{
    public override int ID => 18;

    private float ������deltaValue = 0;
    private float ������deltaValue = 0;

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        ���±����ʺ�����();
    }

    public override void OnTick()
    {
        base.OnTick();
        ���±����ʺ�����();
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.Player.attr.������ -= ������deltaValue;
        GameManager.Instance.Player.attr.���� -= ������deltaValue;
        ������deltaValue = 0;
        ������deltaValue = 0;
    }


    private void ���±����ʺ�����()
    {
        int value = Math.Min((int)GameManager.Instance.Player.attr.���� / 20, 15);

        float ������d = -GameManager.Instance.Player.attr.������ + ������deltaValue;
        float ������d = -GameManager.Instance.Player.attr.���� + ������deltaValue;

        GameManager.Instance.Player.attr.������ = GameManager.Instance.Player.attr.������ - ������deltaValue + value;
        GameManager.Instance.Player.attr.���� = GameManager.Instance.Player.attr.���� - ������deltaValue + value;

        ������d += GameManager.Instance.Player.attr.������;
        ������d += GameManager.Instance.Player.attr.����;

        ������deltaValue = ������d;
        ������deltaValue = ������d;
    }


}
