
// ����������+10%��������+5%
public class һ������ơ�� : BaseItem
{
    public override int ID => 92;

    public override void OnGet()
    {
        base.OnGet();
        attr.��������� += 10;
        attr.���� += 5;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
