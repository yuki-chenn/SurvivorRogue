// ����������+5%��������+2%
public class ��ͨ�Ϲ� : BaseItem
{
    public override int ID => 75;

    public override void OnGet()
    {
        base.OnGet();
        attr.��󱩻��� += 5;
        attr.������ += 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
