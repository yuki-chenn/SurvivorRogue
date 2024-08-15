using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;

public class BaseConditional : Conditional
{

    protected Enemy enemy;

    public override void OnAwake()
    {
        base.OnAwake();
        enemy = gameObject.GetComponent<Enemy>();
    }
}
