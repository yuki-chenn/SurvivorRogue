using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildColliederHandler : MonoBehaviour
{
    public delegate void TriggerAction(Collider2D other);
    public event TriggerAction OnTriggerEnter;
    public event TriggerAction OnTriggerExit;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("child enter!!! " + other.name);
        OnTriggerEnter?.Invoke(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("exit!!!");
        OnTriggerExit?.Invoke(other);
    }
}
