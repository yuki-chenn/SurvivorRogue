using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    public bool move = false;

    public Transform tar;
    public Vector2 dir;

    public float speed = 5;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (move) transform.Translate(dir * Time.deltaTime * speed, Space.World);
    }

    public virtual void StartMove(Transform target)
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        tar = target;
        dir = (tar.position - transform.position).normalized;
        BeforeMove();
        move = true;
    }

    public virtual void StartMove(Vector2 direction)
    {
        dir = direction.normalized;
        BeforeMove();
        move = true;
    }

    protected virtual void BeforeMove() { }
}
