using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 offset;

    private GameObject target
    {
        get
        {
            return GameManager.Instance.playerGo;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = target.transform.position + offset;
        }
    }
}
