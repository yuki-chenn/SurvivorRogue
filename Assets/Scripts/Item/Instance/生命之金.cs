// 将所有的金币1：2转换为最大生命。(无尽模式改为1:0.1)
public class 生命之金 : BaseItem
{
    public override int ID => 96;


    public override void OnGet()
    {
        base.OnGet();
        int val = GameManager.Instance.gameData.money;
        GameManager.Instance.gameData.money = 0;
        if (GameManager.Instance.isEndlessMode(false))
        {
            attr.最大生命 += 0.1f * val;
        }
        else
        {
            attr.最大生命 += 2 * val;
        }
        
    }

}
