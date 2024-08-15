
using UnityEngine;

public class HealthPotionDrop : BaseDrop
{
    private float �ָ���������
    {
        get
        {
            float baseValue = 0.2f;
            #region 53-��������
            // Ѫƿ������Ч������5%
            if (GameManager.Instance.HasItem(ItemEnum.��������))
            {
                baseValue += GameManager.Instance.HasItemNum(ItemEnum.��������) * 0.05f;
            }
            #endregion
            return baseValue;
        }
    }


    public float health
    {
        get
        {
            return GameManager.Instance.Player.attr.������� * �ָ���������;
        }
    }

    protected override void OnPickedUp()
    {
        base.OnPickedUp();
        
        #region 51-����Ѫ��
        // ��ʰȡѪƿʱ������ָ�������10%��ת��Ϊ���������
        if (GameManager.Instance.HasItem(ItemEnum.����Ѫ��))
        {
            if(!GameManager.Instance.isEndlessMode())
            {
                float restore = Mathf.Min(GameManager.Instance.Player.maxHp - GameManager.Instance.Player.curHp, health);
                float overflow = health - restore;
                float addMaxHp = overflow * 0.1f;
                GameManager.Instance.gameData.playerAttr.������� += addMaxHp;
                GameManager.Instance.Player.maxHp = GameManager.Instance.gameData.playerAttr.�������;
            }
        }
        #endregion

        #region 62-֪ʶҩ��
        // ʰȡѪƿʱ�����500����ֵ��
        if (GameManager.Instance.HasItem(ItemEnum.֪ʶҩ��))
        {
            GameManager.Instance.GainExp(500 * GameManager.Instance.HasItemNum(ItemEnum.֪ʶҩ��));
        }
        #endregion

        GameManager.Instance.RestoreHP(health);
    }

}
