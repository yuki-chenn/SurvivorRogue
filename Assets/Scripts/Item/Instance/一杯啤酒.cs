
// ����������+5%��������+2%
public class һ��ơ�� : BaseItem
{
    public override int ID => 74;

    public override void OnGet()
    {
        base.OnGet();
        attr.��������� += 5;
        attr.���� += 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
