using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Χ�ĵ������ 200% �������˺���
public class Skill_Himmel : PlayerAttackObject
{
    protected override float GetBaseDamageAmount()
    {
        return GameManager.Instance.Player.attr.���� * 2;
    }
}
