// 下一场战斗伤害增加40%，在战斗结束后自动出售该道具
public class 主教棋子 : BaseItem
{
    public override int ID => 57;

    private int 主教棋子之力_buff_id = 7;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(主教棋子之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(主教棋子之力_buff_id);
    }




}
