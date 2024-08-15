// 每次商店的第一次刷新免费
public class 商贾之印 : BaseItem
{
    public override int ID => 94;

    public override void OnGet()
    {
        base.OnGet();
        GameManager.Instance.gameData.isNextFree = true;
    }




}
