//ս��ÿ��5�룬ս��ʱ�Ĺ����ٶ�����5 
public class ʱ������� : BaseItem
{
    public override int ID => 83;

    private int ʱ�������֮��_buff_id = 19;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(ʱ�������֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(ʱ�������֮��_buff_id);
    }




}
