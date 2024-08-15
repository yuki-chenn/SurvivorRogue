using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Survivor.Utils;
using BehaviorDesigner.Runtime;

[TaskDescription("具有tag对应的物体是否进入childCollider对应的碰撞器中")]
[TaskCategory("自定义")]
public class Conditional_进入攻击范围 : BaseConditional
{
    public string tag = "";
    public GameObject childCollider;
    private GameObject otherGameObject;
    private bool stayTrigger = false;


    public override void OnAwake()
    {
        base.OnAwake();
        childCollider.GetComponent<ChildColliederHandler>().OnTriggerEnter += triggerEnter;
        childCollider.GetComponent<ChildColliederHandler>().OnTriggerExit += triggerExit;
    }

    public override TaskStatus OnUpdate()
    {
        return stayTrigger ? TaskStatus.Success : TaskStatus.Failure;
    }

    public override void OnEnd()
    {
        
    }

    private void triggerEnter(Collider2D other)
    {
        if (string.IsNullOrEmpty(tag) || other.gameObject.CompareTag(tag))
        {
            otherGameObject = other.gameObject;
            stayTrigger = true;
        }
    }

    private void triggerExit(Collider2D other)
    {
        if (string.IsNullOrEmpty(tag) || other.gameObject.CompareTag(tag))
        {
            otherGameObject = null;
            stayTrigger = false;
        }
    }

    public override void OnReset()
    {
        tag = "";
        otherGameObject = null;
    }
}
