// 暴击率上限+10%，暴击率+5%
public class 邪恶南瓜 : BaseItem
{
    public override int ID => 93;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大暴击率 += 10;
        attr.暴击率 += 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
