using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 追踪目标飞行 : FlyingObject
{

    protected override void Update()
    {
        
        if (tar == null)
        {
            Destroy(gameObject);
            move = false;
            return;
        }
        else
        {
            var tarCharacter = tar.GetComponentInParent<BaseCharacter>();
            if (tarCharacter != null && tarCharacter.isDead)
            {
                Destroy(gameObject);
                move = false;
                //Debug.LogError("Destory dead");
                return;
            }
        }


        if (move)
        {
            dir = (tar.position - transform.position).normalized;
            // 旋转，对准目标
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            base.Update();
            //transform.position = Vector2.MoveTowards(transform.position, tar.position, speed * Time.deltaTime);
        }
            
    }

    public override void StartMove(Transform target)
    {
        tar = target;
        move = true;
    }
}
