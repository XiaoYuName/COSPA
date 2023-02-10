using System.Collections;
using System.Collections.Generic;
using ARPG;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// 扭蛋抽奖界面配置表
/// </summary>
[CreateAssetMenu(fileName = "TwistData",menuName = "ARPG/扭蛋配置")]
public class TwistAnConfig : ScriptableObject
{
    public List<TwistData> TwistDatas = new List<TwistData>();
}

[System.Serializable]
public class TwistData
{
    public TwisType _TwisType;
    [Header("UP标题内容"),ResizableTextArea]
    public string TitleString;
    [Header("描述"),ResizableTextArea]
    public string description;
}
