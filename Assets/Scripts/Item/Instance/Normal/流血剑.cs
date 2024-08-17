
// ±©»÷ÉËº¦+2%£¬ÖÇÁ¦-1
public class Á÷Ñª½£ : BaseItem
{
    public override int ID => 18;

    public override void OnGet()
    {
        base.OnGet();
        attr.±©»÷ÉËº¦ += 2;
        attr.ÖÇÁ¦ -= 1;
    }

    public override void OnDiscard()
    {
        base.OnDiscard();
    }




}
