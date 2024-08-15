
// 每次受到伤害，有50%的概率让攻击者受到10%反弹的伤害
public class 反击之盾 : BaseItem
{
    public override int ID => 86;

    private int 反击之盾之力_buff_id = 22;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(反击之盾之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(反击之盾之力_buff_id);
    }




}
