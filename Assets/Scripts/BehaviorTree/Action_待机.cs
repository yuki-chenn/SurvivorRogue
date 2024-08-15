using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;

public class Action_待机 : BaseAction
{


    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override TaskStatus OnUpdate()
    {
        enemy.待机();
        return TaskStatus.Success;
    }
}
