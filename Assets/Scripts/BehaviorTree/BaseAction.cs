using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;
using BehaviorDesigner.Runtime;

public class BaseAction : Action
{
    protected Enemy enemy;

    public override void OnAwake()
    {
        base.OnAwake();
        enemy = gameObject.GetComponent<Enemy>();
    }
}
