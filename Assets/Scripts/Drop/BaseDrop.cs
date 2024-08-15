using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BaseDrop : MonoBehaviour
{
    // ��Ծ����
    public float jumpHeight = 1f;
    public float jumpDuration = 0.8f;
    public float moveXDistance = 1.5f;
    public float moveYDistance = 0.8f;
    public int jumpCount = 1;
    public Ease ease = Ease.OutCubic;


    // ʰȡ
    protected bool canPick = false;
    protected bool isPicked = false;
    public float pickSpeed = 10.0f;

    protected virtual void Start()
    {
        Jump();
    }

    protected void Update()
    {
        if (isPicked)
        {
            Vector2? playerPos = GameManager.Instance.GetPlayerPosition();
            if (playerPos == null) return;

            var dir = ((Vector2)playerPos - (Vector2)transform.position).normalized;
            transform.Translate(dir * pickSpeed * Time.deltaTime);
        }
    }


    
    protected void Jump()
    {
        // ����Ŀ��λ��
        Vector3 targetPosition = new Vector3(
            transform.position.x + RandomUtil.RandomFloat(moveXDistance), 
            transform.position.y + RandomUtil.RandomFloat(moveYDistance), transform.position.z);
        jumpHeight = RandomUtil.RandomFloat(jumpHeight,false);
        // ʹ�� DOJump ʵ����ԾЧ��
        transform.DOJump(targetPosition, transform.position.y + jumpHeight, jumpCount, jumpDuration)
            .SetEase(ease).OnComplete(()=> { canPick = true; });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isPicked && collision.name == "Core")
            {
                OnPickedUp();
                Destroy(gameObject);
                return;
            }
            if (canPick) isPicked = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isPicked && collision.name == "Core")
            {
                OnPickedUp();
                Destroy(gameObject);
                return;
            }
            if (canPick) isPicked = true;
        }
    }

    protected virtual void OnPickedUp()
    {
        Debug.Log("ʰȡ��:" + gameObject.name);
    }

}
