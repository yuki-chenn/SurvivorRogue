using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class �����˺� : PlayerAttackObject
{

    public BaseWeapon weapon;

    protected override float GetBaseDamageAmount()
    {
        return weapon.������;
    }
}
