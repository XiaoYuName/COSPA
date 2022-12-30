
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

public  class SaveToolEditor: Editor
{
    [MenuItem("Tool/1.打开User表保存路径")]
    public static void OpenUserPath()
    {
        string path = Application.persistentDataPath + "/Sava_GameData";
        if (Directory.Exists(path)) 
        {
            EditorUtility.RevealInFinder(path);
        }
        else
        {
            EditorUtility.DisplayDialog("读取文件夹", "未找到存储路径", "确定");
        }
    }

    
    [MenuItem("Tool/2.打开UnityAPI")]
    public static void OpenUnityUrl()
    {
        Application.OpenURL("https://docs.unity3d.com/cn/2020.2/ScriptReference/index.html");
    }
    [MenuItem("Tool/2.打开C# API")]
    public static void OpenNETUrl()
    {
        Application.OpenURL("https://learn.microsoft.com/zh-cn/dotnet/api/");
    }
    [MenuItem("Tool/3.打开BilBil")]
    public static void OpenBilBil()
    {
        Application.OpenURL("https://www.bilibili.com");
    }
}
