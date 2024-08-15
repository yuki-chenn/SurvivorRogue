using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public class ToolsMenuItem : Editor
{
    [MenuItem("Tools/打开模板表工具",false,10000)]
    public static void OpenTemplateTools()
    {
        UnityEngine.Debug.Log("打开模板表工具");

        // 文件夹路径
        string folderPath = "F:\\java\\JavaProject\\DatabaseTempleteToXml"; // 修改为你的文件夹路径

        try
        {
            // Windows 平台使用 Explorer
            Process.Start("explorer.exe", folderPath);

        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Failed to open folder: " + e.Message);
        }
    }
}
