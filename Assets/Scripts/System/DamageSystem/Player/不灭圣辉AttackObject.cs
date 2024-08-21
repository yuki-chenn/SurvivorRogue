
// 对周围的敌人造成 最大生命 伤害。
public class 不灭圣辉AttackObject : PlayerAttackObject
{
    protected override float GetBaseDamageAmount()
    {
        return GameManager.Instance.Player.attr.最大生命 * GameManager.Instance.gameData.level;
    }
}
