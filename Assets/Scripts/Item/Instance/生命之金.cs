// �����еĽ��1��2ת��Ϊ���������
public class ����֮�� : BaseItem
{
    public override int ID => 96;


    public override void OnGet()
    {
        base.OnGet();
        int val = GameManager.Instance.gameData.money;
        GameManager.Instance.gameData.money = 0;
        attr.������� += 2 * val;
    }

}
