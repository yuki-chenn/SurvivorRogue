// ±©»÷ÉËº¦+4%£¬±©»÷ÂÊ-2%
public class »ÙÃð¾íÖá : BaseItem
{
    public override int ID => 17;

    public override void OnGet()
    {
        base.OnGet();
        attr.±©»÷ÉËº¦ += 4;
        attr.±©»÷ÂÊ -= 2;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
