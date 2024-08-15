
// 对范围内的敌人造成50%智力的持续伤害 
public class Skill星落 : PlayerAttackObject
{
    protected override float GetBaseDamageAmount()
    {
        return GameManager.Instance.Player.attr.智力 * 0.5f;
    }
}
