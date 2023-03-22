using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
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

    /// <summary>
    /// 清空所有子物体
    /// </summary>
    /// <param name="transform">子物体的父级物体</param>
    public static void Clear(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
    
    /// <summary>
    /// 播放当前角色Spine动画
    /// </summary>
    /// <param name="playName">动画名称</param>
    public  static void PlaySpineAnimation(SkeletonGraphic SpineController,string playName,bool isLoop)
    {
        SpineController.AnimationState.SetAnimation(0, playName, isLoop);
    }
}
