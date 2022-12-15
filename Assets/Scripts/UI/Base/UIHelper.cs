using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI公用函数类:
///     包含一些常用功能的定义
/// </summary>
public static class UIHelper
{

    /// <summary>
    /// 清空所有子物体
    /// </summary>
    /// <param name="transform"></param>
    public static void Clear(RectTransform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
}
