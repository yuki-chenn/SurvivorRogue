using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ײ��Ϊ��������buff
public class BuffAttackObject : BaseAttackObject 
{
    public int buffId = -1;

    public List<string> collideTag;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collideTag.Contains(collision.tag))
        {
            if(buffId == -1)
            {
                Debug.LogError("buff id Ϊ -1,����Ϊ" + transform.parent.name + "����");
                return;
            }
            collision.GetComponentInParent<BaseCharacter>().buffList.AddBuff(buffId, source, collision.transform.parent.gameObject);
        }

    }
}
