
// 下一场战斗伤害增加10%，在战斗结束后自动出售该道具
public class 士兵棋子 : BaseItem
{
    public override int ID => 54;

    private int 士兵棋子之力_buff_id = 6;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(士兵棋子之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(士兵棋子之力_buff_id);
    }




}
