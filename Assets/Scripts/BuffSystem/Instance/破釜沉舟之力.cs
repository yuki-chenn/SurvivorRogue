using System;

[Serializable]
// ±©»÷ÉËº¦Ôö¼Ó50%
public class ÆÆ¸ª³ÁÖÛÖ®Á¦ : BaseBuff
{
    public override int ID => 20;

    private float deltaValue = 0;

    public override void OnAdd()
    {
        base.OnAdd();
        if (deltaValue + float.Epsilon < Info.MaxStack * 50)
        {
            deltaValue = curStack * 50;
            GameManager.Instance.Player.attr.±©»÷ÉËº¦ += curStack * 50;
        }
    }

    public override void OnRemove()
    {
        base.OnRemove();
        GameManager.Instance.Player.attr.±©»÷ÉËº¦ -= deltaValue;
    }

}
