// 造成的伤害增加100%，但是受到的伤害也增加50%
public class 力量代价 : BaseItem
{
    public override int ID => 87;

    private int 力量代价之力_buff_id = 23;
    private int 力量代价之咒_buff_id = 24;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(力量代价之力_buff_id, null, GameManager.Instance.playerGo);
        player.buffList.AddBuff(力量代价之咒_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(力量代价之力_buff_id);
        player.buffList.RemoveBuff(力量代价之咒_buff_id);
    }




}
