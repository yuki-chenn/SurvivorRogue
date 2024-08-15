using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;

public class Action_死亡 : BaseAction
{
    public float delayDestory;

    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override TaskStatus OnUpdate()
    {
        enemy.Die(delayDestory);
        return TaskStatus.Success;
    }
}
