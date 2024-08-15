using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;
using BehaviorDesigner.Runtime;

public class Conditional_死亡 : BaseConditional
{

    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override TaskStatus OnUpdate()
    {
        return enemy.curHp > 0 ? TaskStatus.Failure : TaskStatus.Success;
    }
}
