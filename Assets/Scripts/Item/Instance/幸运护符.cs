
// 每拥有20点幸运，战斗时的暴击率增加1%，闪避率增加1%，最多增加10%，且不会超过暴击率和闪避率上限
public class 幸运护符 : BaseItem
{
    public override int ID => 82;

    private int 幸运护符之力_buff_id = 18;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(幸运护符之力_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(幸运护符之力_buff_id);
    }




}
