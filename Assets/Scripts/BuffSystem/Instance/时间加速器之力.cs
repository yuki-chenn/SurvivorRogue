using System;

[Serializable]
// 战斗时每过5秒，战斗时的攻击速度增加5
public class 时间加速器之力 : BaseBuff
{
    public override int ID => 19;

    private float deltaValue = 0;

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        // 重置计时器
        clkTick = Info.TickTime;
        deltaValue = 0;
    }

    public override void OnTick()
    {
        base.OnTick();
        float d = -GameManager.Instance.Player.attr.攻击速度;
        GameManager.Instance.Player.attr.攻击速度 += 5;
        d += GameManager.Instance.Player.attr.攻击速度;
        deltaValue += d;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.Player.attr.攻击速度 -= deltaValue;
        deltaValue = 0;
    }


}
