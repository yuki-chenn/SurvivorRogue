// �����еĽ��1��2ת��Ϊ���������(�޾�ģʽ��Ϊ1:0.1)
public class ����֮�� : BaseItem
{
    public override int ID => 96;


    public override void OnGet()
    {
        base.OnGet();
        int val = GameManager.Instance.gameData.money;
        GameManager.Instance.gameData.money = 0;
        if (GameManager.Instance.isEndlessMode(false))
        {
            attr.������� += 0.1f * val;
        }
        else
        {
            attr.������� += 2 * val;
        }
        
    }

}
