using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;
using BehaviorDesigner.Runtime;

public class Action_计时器 : BaseAction
{

    public SharedFloat durationTime = 1;

    private float timer = 0;

    public override TaskStatus OnUpdate()
    {
        timer += Time.deltaTime;
        TaskStatus status = timer < durationTime.Value ? TaskStatus.Success : TaskStatus.Failure;
        if (status == TaskStatus.Failure) timer = 0;
        return status;
    }

    public override void OnReset()
    {
        base.OnReset();
        Debug.LogError("OnReset");
        timer = 0;
    }
}
