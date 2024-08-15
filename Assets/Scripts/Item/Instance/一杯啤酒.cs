
// 闪避率上限+5%，闪避率+2%
public class 一杯啤酒 : BaseItem
{
    public override int ID => 74;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大闪避率 += 5;
        attr.闪避 += 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
