
using UnityEngine;

public class HealthPotionDrop : BaseDrop
{
    private float 恢复生命倍率
    {
        get
        {
            float baseValue = 0.2f;
            #region 53-生命番茄
            // 血瓶的治疗效果增加5%
            if (GameManager.Instance.HasItem(ItemEnum.生命番茄))
            {
                baseValue += GameManager.Instance.HasItemNum(ItemEnum.生命番茄) * 0.05f;
            }
            #endregion
            return baseValue;
        }
    }


    public float health
    {
        get
        {
            return GameManager.Instance.Player.attr.最大生命 * 恢复生命倍率;
        }
    }

    protected override void OnPickedUp()
    {
        base.OnPickedUp();
        
        #region 51-永恒血冠
        // 当拾取血瓶时，溢出恢复生命的10%将转换为最大生命。
        if (GameManager.Instance.HasItem(ItemEnum.永恒血冠))
        {
            if(!GameManager.Instance.isEndlessMode())
            {
                float restore = Mathf.Min(GameManager.Instance.Player.maxHp - GameManager.Instance.Player.curHp, health);
                float overflow = health - restore;
                float addMaxHp = overflow * 0.1f;
                GameManager.Instance.gameData.playerAttr.最大生命 += addMaxHp;
                GameManager.Instance.Player.maxHp = GameManager.Instance.gameData.playerAttr.最大生命;
            }
        }
        #endregion

        #region 62-知识药剂
        // 拾取血瓶时，获得500经验值。
        if (GameManager.Instance.HasItem(ItemEnum.知识药剂))
        {
            GameManager.Instance.GainExp(500 * GameManager.Instance.HasItemNum(ItemEnum.知识药剂));
        }
        #endregion

        GameManager.Instance.RestoreHP(health);
    }

}
