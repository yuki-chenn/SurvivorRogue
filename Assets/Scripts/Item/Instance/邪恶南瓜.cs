// ����������+10%��������+5%
public class а���Ϲ� : BaseItem
{
    public override int ID => 93;

    public override void OnGet()
    {
        base.OnGet();
        attr.��󱩻��� += 10;
        attr.������ += 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
