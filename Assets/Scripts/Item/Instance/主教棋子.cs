// ��һ��ս���˺�����40%����ս���������Զ����۸õ���
public class �������� : BaseItem
{
    public override int ID => 57;

    private int ��������֮��_buff_id = 7;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(��������֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(��������֮��_buff_id);
    }




}
