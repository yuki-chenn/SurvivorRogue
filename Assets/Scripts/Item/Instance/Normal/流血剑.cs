
// �����˺�+2%������-1
public class ��Ѫ�� : BaseItem
{
    public override int ID => 18;

    public override void OnGet()
    {
        base.OnGet();
        attr.�����˺� += 2;
        attr.���� -= 1;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
