using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class 弹出后渐隐 : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public float colorFadedSpeed;
    public bool destory;

    private float clk;

    private TextMeshPro tmp;
    private Color curColor;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        if (tmp != null) curColor = tmp.color; 
    }

    private void Update()
    {
        if(clk > lifetime)
        {
            if (destory) Destroy(gameObject);
            else gameObject.SetActive(false);
            return;
        }
        clk += Time.deltaTime;
        transform.Translate(speed * Time.deltaTime * Vector2.up);
        if (tmp != null)
        {
            curColor.a -= colorFadedSpeed * Time.deltaTime;
            tmp.color = curColor;
        }
        
    }

}
