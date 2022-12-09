using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CopyHierarchyPaht : Editor
{
    private static GameObject targetObj;
    [MenuItem("GameObject/CopyHiPath",priority =3)]
    public static void CopyHiPath()
    {
        targetObj = Selection.activeGameObject;
        UnityEngine.Object obj = Selection.activeObject;
        if (obj == null)
        {
            Debug.LogError("You must select Obj first!");
            return;
        }
        string result = AssetDatabase.GetAssetPath(obj);
        if (string.IsNullOrEmpty(result))//如果不是资源则在场景中查找
        {
            Transform selectChild = Selection.activeTransform;
            if (selectChild != null)
            {
                result = selectChild.name;
                while (selectChild.parent != null)
                {
                    selectChild = selectChild.parent;
                    result = $"{selectChild.name}/{result}";
                }
            }
        }
        ClipBoard.Copy(result);
        Debug.Log($"The gameobject:{obj.name}'s path has been copied to the clipboard!");
    }
    
    /// <summary>
    /// 剪切板
    /// </summary>
    public class ClipBoard
    {
        /// <summary>
        /// 将信息复制到剪切板当中
        /// </summary>
        public static void Copy(string format,params object[] args)
        {
            string result = string.Format(format,args);

            GUIUtility.systemCopyBuffer = result;
        }
    }
}
