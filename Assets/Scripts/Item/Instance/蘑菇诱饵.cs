
// 战斗开始时，在随机位置召唤一个蘑菇怪人。
public class 蘑菇诱饵 : BaseItem
{
    public override int ID => 66;

    private int 蘑菇诱饵之力_buff_id = 14;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.Player.buffList.AddBuff(蘑菇诱饵之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        GameManager.Instance.Player.buffList.RemoveBuff(蘑菇诱饵之力_buff_id);
    }




}
