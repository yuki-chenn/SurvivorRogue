// ��ɵ��˺�����100%�������ܵ����˺�Ҳ����50%
public class �������� : BaseItem
{
    public override int ID => 87;

    private int ��������֮��_buff_id = 23;
    private int ��������֮��_buff_id = 24;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(��������֮��_buff_id, null, GameManager.Instance.playerGo);
        player.buffList.AddBuff(��������֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(��������֮��_buff_id);
        player.buffList.RemoveBuff(��������֮��_buff_id);
    }




}
