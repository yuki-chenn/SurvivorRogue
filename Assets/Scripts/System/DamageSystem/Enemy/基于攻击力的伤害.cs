using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 基于攻击力的伤害 : EnemyAttackObject
{

    protected override float GetBaseDamageAmount()
    {
        return EnemyAttr.攻击力;
    }
}
