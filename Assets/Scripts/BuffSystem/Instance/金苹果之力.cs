using System;


// ÿӵ��20��ң�ս��ʱ��ɵ��˺�����2.5%
[Serializable]
public class ��ƻ��֮�� : BaseBuff
{
    public override int ID => 17;

    public override void AddModify()
    {
        AddStack();
    }

    public override void OnBeforeGiveDamage(DamageInfo info)
    {
        base.OnBeforeGiveDamage(info);
        int times = GameManager.Instance.gameData.money / 20;
        info.bonus += curStack * 2.5f * times;
    }

}
