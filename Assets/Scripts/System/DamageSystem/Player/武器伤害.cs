using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ÎäÆ÷ÉËº¦ : PlayerAttackObject
{

    public BaseWeapon weapon;

    protected override float GetBaseDamageAmount()
    {
        return weapon.¹¥»÷Á¦;
    }
}
