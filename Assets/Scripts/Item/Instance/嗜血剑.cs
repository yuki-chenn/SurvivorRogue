
// 每次击败一个敌人时，恢复5点生命值
public class 嗜血剑 : BaseItem
{
    public override int ID => 69;

    private int 嗜血剑之力_buff_id = 16;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(嗜血剑之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(嗜血剑之力_buff_id);
    }




}
