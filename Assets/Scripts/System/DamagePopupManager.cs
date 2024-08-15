using Survivor.Base;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupManager : Singleton<DamagePopupManager>
{

    public GameObject damagePopupPrefab;
    public GameObject restorePopupPrefab;

    private int _count;
    private int Count
    {
        get { return _count; }
        set
        {
            if (_count > 1000) _count = 0;
            else _count = value;
        }
    }

    public void CreateDamageHint(Vector2 pos, DamageInfo damage)
    {
        string txtDamage = string.Format("{0}{1}",damage.isCritical && !damage.isMiss ? "<sprite=0>" : "", 
            damage.isMiss ? "Miss" : damage.Value.ToString("F0")); 
        var popup = Instantiate(damagePopupPrefab, ContainerManager.Instance.damageHintContainer);
        popup.transform.position = pos;
        popup.GetComponent<TextMeshPro>().SetText(txtDamage);
        popup.GetComponent<TextMeshPro>().sortingOrder = Count++;      
    }
    public void CreateRestoreHint(Vector2 pos, float value)
    {
        string txtDamage = value.ToString("F0");
        var popup = Instantiate(restorePopupPrefab, ContainerManager.Instance.damageHintContainer);
        popup.transform.position = pos;
        popup.GetComponent<TextMeshPro>().SetText(txtDamage);
        popup.GetComponent<TextMeshPro>().sortingOrder = Count++;
    }
}
