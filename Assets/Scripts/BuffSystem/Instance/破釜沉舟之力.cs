using System;

[Serializable]
// �����˺�����50%
public class �Ƹ�����֮�� : BaseBuff
{
    public override int ID => 20;

    private float deltaValue = 0;

    public override void OnAdd()
    {
        base.OnAdd();
        if (deltaValue + float.Epsilon < Info.MaxStack * 50)
        {
            deltaValue = curStack * 50;
            GameManager.Instance.Player.attr.�����˺� += curStack * 50;
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.attr.�����˺� -= deltaValue;
    }

}
