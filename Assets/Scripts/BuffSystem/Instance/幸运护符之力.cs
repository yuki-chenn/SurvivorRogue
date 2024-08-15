
// 每拥有20点幸运，战斗时的暴击率增加1%，闪避率增加1%，最多增加15%，且不会超过暴击率和闪避率上限
using System;

[Serializable]
public class 幸运护符之力 : BaseBuff
{
    public override int ID => 18;

    private float 暴击率deltaValue = 0;
    private float 闪避率deltaValue = 0;

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        更新暴击率和闪避();
    }

    public override void OnTick()
    {
        base.OnTick();
        更新暴击率和闪避();
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.Player.attr.暴击率 -= 暴击率deltaValue;
        GameManager.Instance.Player.attr.闪避 -= 闪避率deltaValue;
        暴击率deltaValue = 0;
        闪避率deltaValue = 0;
    }


    private void 更新暴击率和闪避()
    {
        int value = Math.Min((int)GameManager.Instance.Player.attr.幸运 / 20, 15);

        float 暴击率d = -GameManager.Instance.Player.attr.暴击率 + 暴击率deltaValue;
        float 闪避率d = -GameManager.Instance.Player.attr.闪避 + 闪避率deltaValue;

        GameManager.Instance.Player.attr.暴击率 = GameManager.Instance.Player.attr.暴击率 - 暴击率deltaValue + value;
        GameManager.Instance.Player.attr.闪避 = GameManager.Instance.Player.attr.闪避 - 闪避率deltaValue + value;

        暴击率d += GameManager.Instance.Player.attr.暴击率;
        闪避率d += GameManager.Instance.Player.attr.闪避;

        暴击率deltaValue = 暴击率d;
        闪避率deltaValue = 闪避率d;
    }


}
