
// ������+2%�������˺�-4%
public class Ҫ����ͷ : BaseItem
{
    public override int ID => 15;

    public override void OnGet()
    {
        base.OnGet();
        attr.������ += 2;
        attr.�����˺� -= 4;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
