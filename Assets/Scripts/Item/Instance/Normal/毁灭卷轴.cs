// �����˺�+4%��������-2%
public class ������� : BaseItem
{
    public override int ID => 17;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����˺� += 4;
        attr.������ -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
