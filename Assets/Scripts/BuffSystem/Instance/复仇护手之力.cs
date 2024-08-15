using System;

[Serializable]
// ÿ���ܵ�����������2%�ı����ʣ��������20%������5s��
public class ������֮�� : BaseBuff
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
            float d = -GameManager.Instance.Player.attr.������;
            GameManager.Instance.Player.attr.������ += 2;
            d += GameManager.Instance.Player.attr.������;
            deltaValue += d;
        }
        
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.attr.������ -= deltaValue;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
    }

}
