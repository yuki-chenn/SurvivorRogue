using System;


// ���� 
// 2s�ڵֵ����й������˺�
[Serializable]
public class ����ħ�� : BaseBuff
{
    public override int ID => 26;

    public override void OnBeforeTakeDamage(DamageInfo info)
    {
        base.OnBeforeTakeDamage(info);
        info.bonus = 0;
    }

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        needRemove = true;
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.OnPlayerReset();
    }

}
