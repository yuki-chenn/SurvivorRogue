using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PlayerAttribute
{
    public float maxhp = 100;
    public float attackSpeed = 100;
    public float strength = 10;
    public float intelligence = 10;
    public float defense = 5;
    public float dodge = 5;
    public float critRate = 5;
    public float critDamage = 150;
    public float moveSpeed = 100;
    public float luck = 5;

    public float maxDodgeRate = 25;
    public float maxCriticalRate = 30;



    public float 最大闪避率
    {
        get { return maxDodgeRate; }
        set { maxDodgeRate = ClampValue(value, 25, 80); }
    }
    public float 最大暴击率
    {
        get { return maxCriticalRate; }
        set { maxCriticalRate = ClampValue(value, 30, 80); }
    }

    // 最大生命大于0
    public float 最大生命
    {
        get { return maxhp; }
        set { maxhp = ClampValue(value, float.Epsilon, float.MaxValue); }
    }

    // 攻击速度 >0 <=250
    public float 攻击速度
    {
        get { return attackSpeed; }
        set { attackSpeed = ClampValue(value, float.Epsilon, 250); }
    }

    // 力量任意
    public float 力量
    {
        get
        {
            #region 48-千年天秤
            // 你的力量和智力的数值都永远等于两者中较高的一个。
            if (GameManager.Instance.HasItem(ItemEnum.千年天秤))
            {
                return Mathf.Max(strength, intelligence);
            }

            #endregion
            return strength;
        }
        set { strength = value; } 
    }

    // 智力任意
    public float 智力
    {
        get
        {
            #region 48-千年天秤
            // 你的力量和智力的数值都永远等于两者中较高的一个。
            if (GameManager.Instance.HasItem(ItemEnum.千年天秤))
            {
                return Mathf.Max(strength, intelligence);
            }
            #endregion

            return intelligence;
        }
        set { intelligence = value; } 
    }

    // 防御 >=0
    public float 防御
    {
        get { return defense; }
        set { defense = ClampValue(value, 0, float.MaxValue); }
    }

    // 闪避 >=0 <=25
    public float 闪避
    {
        get { return dodge; }
        set { dodge = ClampValue(value, 0, 最大闪避率); }
    }

    // 暴击率 >=0 <=30
    public float 暴击率
    {
        get { return critRate; }
        set { critRate = ClampValue(value, 0, 最大暴击率); }
    }

    // 暴击伤害 >=100
    public float 暴击伤害
    {
        get { return critDamage; }
        set { critDamage = ClampValue(value, 100, float.MaxValue); }
    }

    // 移动速度 >0 <=250
    public float 移动速度
    {
        get { return moveSpeed; }
        set { moveSpeed = ClampValue(value, float.Epsilon, 250); }
    }

    // 幸运任意
    public float 幸运
    {
        get { return luck; }
        set { luck = value; } // 任意值不需要 Clamp
    }

    private float ClampValue(float value, float min = float.MinValue, float max = float.MaxValue)
    {
        return Mathf.Clamp(value, min, max);
    }

    public PlayerAttribute DeepCopy()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, this);
            ms.Seek(0, SeekOrigin.Begin);
            return (PlayerAttribute)formatter.Deserialize(ms);
        }
    }
}

