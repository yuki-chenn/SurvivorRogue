
// ÿӵ��20�����ˣ�ս��ʱ�ı���������1%������������1%���������10%���Ҳ��ᳬ�������ʺ�����������
public class ���˻��� : BaseItem
{
    public override int ID => 82;

    private int ���˻���֮��_buff_id = 18;

    public override void OnGet()
    {
        base.OnGet();
        player.buffList.AddBuff(���˻���֮��_buff_id, null, GameManager.Instance.playerGo);
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
        player.buffList.RemoveBuff(���˻���֮��_buff_id);
    }




}
