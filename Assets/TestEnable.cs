using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.LogError(gameObject.name + " OnEnable");
        Debug.LogError(StackTraceUtility.ExtractStackTrace());
    }

    private void OnDisable()
    {
        Debug.LogError(gameObject.name + " OnDisable");
    }
}
