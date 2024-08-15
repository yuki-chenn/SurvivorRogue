// 将所有的金币1：2转换为最大生命。
public class 生命之金 : BaseItem
{
    public override int ID => 96;


    public override void OnGet()
    {
        base.OnGet();
        int val = GameManager.Instance.gameData.money;
        GameManager.Instance.gameData.money = 0;
        attr.最大生命 += 2 * val;
    }

}
