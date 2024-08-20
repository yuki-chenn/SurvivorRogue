using System.Collections;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    private float _deltaTime = 0.0f;
    private float _fps = 0.0f;
    private float updateInterval = 0.5f; // ���¼�������룩
    private GUIStyle _style;
    private string _text;
    private float _msec;

    private void Start()
    {
        _style = new GUIStyle();
        _style.alignment = TextAnchor.UpperLeft;
        _style.fontSize = (int)(Screen.height * 0.02f);
        _style.normal.textColor = Color.white;

        StartCoroutine(UpdateFPS());
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(updateInterval); // ������ת��Ϊ��
            _msec = _deltaTime * 1000.0f;
            _fps = 1.0f / _deltaTime;
            _text = string.Format("{0:0.0} ms ({1:0.} fps)", _msec, _fps);
        }
    }

    private void Update()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        int fontSize = Mathf.Max((int)(h * 0.03f), 20); // ���������СΪ��Ļ�߶ȵ�3%����СΪ20
        _style.fontSize = fontSize;

        Rect rect = new Rect(0, h * 0.5f, 200, h * 0.02f);
        GUI.Label(rect, _text, _style);
    }
}

