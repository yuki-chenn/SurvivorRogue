
// ÿ���ܵ��˺�����50%�ĸ����ù������ܵ�10%�������˺�
public class ����֮�� : BaseItem
{
    public override int ID => 86;

    private int ����֮��֮��_buff_id = 22;

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
