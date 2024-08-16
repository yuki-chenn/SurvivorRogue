using Survivor.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickController : ScrollRect
{
    // °ë¾¶
    private float radius = 80.0f;

    private Transform focusTrans;

    protected override void Awake()
    {
        focusTrans = transform.Find("Focus");
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        Vector2 dragDir = content.anchoredPosition.normalized;
        if(content.anchoredPosition.magnitude > radius)
        {
            SetContentAnchoredPosition(dragDir * radius);
        }
        GameManager.Instance.SetMoveDir(dragDir);
        int quadrant = GeometryUtil.GetQuadrant(dragDir);
        RefreshFocus(quadrant);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        GameManager.Instance.SetMoveDir(Vector2.zero);
        RefreshFocus(0);
    }

    private void RefreshFocus(int quadrant)
    {
        for(int i = 0; i < focusTrans.childCount; ++i)
        {
            focusTrans.GetChild(i).gameObject.SetActive(i == quadrant - 1);
        }
    }
}
