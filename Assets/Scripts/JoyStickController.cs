using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickController : ScrollRect
{
    // °ë¾¶
    private float radius = 70.0f;

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        Vector2 dragDir = content.anchoredPosition.normalized;
        if(content.anchoredPosition.magnitude > radius)
        {
            SetContentAnchoredPosition(dragDir * radius);
        }
        GameManager.Instance.SetMoveDir(dragDir);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        GameManager.Instance.SetMoveDir(Vector2.zero);
    }
}
