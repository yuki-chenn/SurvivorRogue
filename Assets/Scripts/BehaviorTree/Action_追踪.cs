using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;
using BehaviorDesigner.Runtime;

public class Action_追踪 : BaseAction
{

    public override TaskStatus OnUpdate()
    {
        enemy.移动();
        return TaskStatus.Success;
    }
}
