
using System;


// ���ο�ʼʱ������ֵΪ20%
[Serializable]
public class Ѫ֮��Լ֮�� : BaseBuff
{
    public override int ID => 3;

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        GameManager.Instance.Player.curHp = GameManager.Instance.gameData.playerAttr.������� * 0.2f;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
    }

}
