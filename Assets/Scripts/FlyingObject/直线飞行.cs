using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ö±Ïß·ÉÐÐ : FlyingObject
{
    protected override void BeforeMove()
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
