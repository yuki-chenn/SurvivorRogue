using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttackObject : MonoBehaviour
{
    public GameObject source;

    public bool autoDestory;

    public bool isFlyingObject;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            if(isFlyingObject) Destroy(gameObject);
            return;
        }
    }

}
