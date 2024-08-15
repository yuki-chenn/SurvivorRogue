using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;

public class Action_近战攻击 : BaseAction
{


    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override TaskStatus OnUpdate()
    {
        enemy.近战攻击();
        return TaskStatus.Success;
    }
}
