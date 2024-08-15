
// 闪避率上限+10%，闪避率+5%
public class 一杯神秘啤酒 : BaseItem
{
    public override int ID => 92;

    public override void OnGet()
    {
        base.OnGet();
        attr.最大闪避率 += 10;
        attr.闪避 += 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
