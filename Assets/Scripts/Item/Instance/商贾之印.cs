// ÿ���̵�ĵ�һ��ˢ�����
public class �̼�֮ӡ : BaseItem
{
    public override int ID => 94;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.isNextFree = true;
    }




}
