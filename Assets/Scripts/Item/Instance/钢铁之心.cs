// ÿ���ܵ��˺���ս��ʱ�ķ���������1���������30
public class ����֮�� : BaseItem
{
    public override int ID => 85;

    private int ����֮��֮��_buff_id = 21;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(����֮��֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(����֮��֮��_buff_id);
    }




}
