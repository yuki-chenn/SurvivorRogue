
// ս����ʼʱ�������λ���ٻ�һ��Ģ�����ˡ�
public class Ģ���ն� : BaseItem
{
    public override int ID => 66;

    private int Ģ���ն�֮��_buff_id = 14;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.Player.buffList.AddBuff(Ģ���ն�֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        GameManager.Instance.Player.buffList.RemoveBuff(Ģ���ն�֮��_buff_id);
    }




}
