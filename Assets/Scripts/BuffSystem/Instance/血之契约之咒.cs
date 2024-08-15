
using System;


// 波次开始时，生命值为20%
[Serializable]
public class 血之契约之咒 : BaseBuff
{
    public override int ID => 3;

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        GameManager.Instance.Player.curHp = GameManager.Instance.gameData.playerAttr.最大生命 * 0.2f;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
    }

}
