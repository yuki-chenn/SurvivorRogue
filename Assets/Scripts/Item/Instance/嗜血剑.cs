
// ÿ�λ���һ������ʱ���ָ�5������ֵ
public class ��Ѫ�� : BaseItem
{
    public override int ID => 69;

    private int ��Ѫ��֮��_buff_id = 16;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(��Ѫ��֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(��Ѫ��֮��_buff_id);
    }




}
