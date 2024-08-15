using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class ToolsMenuItem : Editor
{
    [MenuItem("Tools/��ģ�����",false,10000)]
    public static void OpenTemplateTools()
    {
        UnityEngine.Debug.Log("��ģ�����");

        // �ļ���·��
        string folderPath = "F:\\java\\JavaProject\\DatabaseTempleteToXml"; // �޸�Ϊ����ļ���·��

        try
        {
            // Windows ƽ̨ʹ�� Explorer
            Process.Start("explorer.exe", folderPath);

        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Failed to open folder: " + e.Message);
        }
    }
}
