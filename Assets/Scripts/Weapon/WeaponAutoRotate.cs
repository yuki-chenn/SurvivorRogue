using Survivor.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutoRotate : MonoBehaviour
{
    public bool clockwise;

    public float radius = 5.0f;
    private List<float> initialAngle;

    private float rotateAngle = 0.0f;
    public float speed = 1.0f;

    private bool rotating = false;


    private void OnDisable()
    {
        StopRotate();
    }

    private void OnEnable()
    {
        StartRotate();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotating) return;
        if (rotateAngle >= 360.0f || rotateAngle <= -360.0f) rotateAngle = 0.0f;
        if(clockwise) rotateAngle -= speed * Time.deltaTime;
        else rotateAngle += speed * Time.deltaTime;
        RefreashWeapons();
    }


    void RefreashWeapons()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            var weapon = transform.GetChild(i);
            if (!weapon.GetComponent<BaseWeapon>().controlled) continue;
            var initialAngle = 360.0f / transform.childCount * i;
            float curAngle = initialAngle + rotateAngle;
            weapon.localPosition = GeometryUtil.GetRadPosition(
                GeometryUtil.Angle2Rad(curAngle), radius);
        }
    }

    public void StartRotate()
    {
        rotating = true;
    }
    public void StopRotate()
    {
        rotating = false;
    }


}
