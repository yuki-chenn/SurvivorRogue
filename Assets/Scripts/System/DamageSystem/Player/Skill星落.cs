
// �Է�Χ�ڵĵ������50%�����ĳ����˺� 
public class Skill���� : PlayerAttackObject
{
    protected override float GetBaseDamageAmount()
    {
        return GameManager.Instance.Player.attr.���� * 0.5f;
    }
}
