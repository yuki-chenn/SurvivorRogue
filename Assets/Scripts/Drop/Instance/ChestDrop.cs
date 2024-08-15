
public class ChestDrop : BaseDrop
{
    public RankType rankType;
    public int money;

    protected override void OnPickedUp()
    {
        base.OnPickedUp();
        GameManager.Instance.PickUpMoney(money);
        GameManager.Instance.PickUpChest(rankType);
    }

}
