using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 对周围的敌人造成 200% 的力量伤害。
public class Skill_Himmel : PlayerAttackObject
{
    protected override float GetBaseDamageAmount()
    {
        return GameManager.Instance.Player.attr.力量 * 2;
    }
}
