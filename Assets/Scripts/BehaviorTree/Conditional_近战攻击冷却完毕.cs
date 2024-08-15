using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;
using BehaviorDesigner.Runtime;

public class Conditional_近战攻击冷却完毕 : BaseConditional
{

    public override void OnAwake()
    {
        base.OnAwake();
    }

    public override TaskStatus OnUpdate()
    {
        return enemy.近战攻击冷却完毕 ? TaskStatus.Success : TaskStatus.Failure;
    }
}
