// 暴击率上限+5%，暴击率+2%
public class 普通南瓜 : BaseItem
{
    public override int ID => 75;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大暴击率 += 5;
        attr.暴击率 += 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
