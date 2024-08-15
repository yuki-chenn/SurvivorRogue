// 每次受到伤害，战斗时的防御力增加1，最多增加30
public class 钢铁之心 : BaseItem
{
    public override int ID => 85;

    private int 钢铁之心之力_buff_id = 21;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(钢铁之心之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(钢铁之心之力_buff_id);
    }




}
