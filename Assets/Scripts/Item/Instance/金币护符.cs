
// �ܵ�����ʱ��10%�ĸ�������1ö���
public class ��һ��� : BaseItem
{
    public override int ID => 1;

    private int ��һ���֮��_buff_id = 2;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(��һ���֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(��һ���֮��_buff_id);
    }




}
