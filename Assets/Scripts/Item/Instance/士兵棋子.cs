
// ��һ��ս���˺�����10%����ս���������Զ����۸õ���
public class ʿ������ : BaseItem
{
    public override int ID => 54;

    private int ʿ������֮��_buff_id = 6;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(ʿ������֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(ʿ������֮��_buff_id);
    }




}
