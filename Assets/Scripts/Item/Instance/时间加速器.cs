//战斗每过5秒，战斗时的攻击速度增加5 
public class 时间加速器 : BaseItem
{
    public override int ID => 83;

    private int 时间加速器之力_buff_id = 19;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(时间加速器之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(时间加速器之力_buff_id);
    }




}
