
// 受到攻击时有10%的概率生成1枚金币
public class 金币护符 : BaseItem
{
    public override int ID => 1;

    private int 金币护符之力_buff_id = 2;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(金币护符之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(金币护符之力_buff_id);
    }




}
