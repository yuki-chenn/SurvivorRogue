
// ����Χ�ĵ������ ������� �˺���
public class ����ʥ��AttackObject : PlayerAttackObject
{
    protected override float GetBaseDamageAmount()
    {
        return GameManager.Instance.Player.attr.������� * GameManager.Instance.gameData.level;
    }
}
