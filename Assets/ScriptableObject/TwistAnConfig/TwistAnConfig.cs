using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// 扭蛋抽奖界面配置表
/// </summary>
[CreateAssetMenu(fileName = "TwistData",menuName = "ARPG/扭蛋配置")]
public class TwistAnConfig : ScriptableObject
{
    public List<TwistData> TwistDatas = new List<TwistData>();
    
    public List<TwistDouble> Settings = new List<TwistDouble>();
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

/// <summary>
/// 概率设定与卡池设定
/// </summary>
[System.Serializable]
public class TwistDouble
{
    public TwisType _TwisType;
    
    [Header("当期Up 六星(七彩)角色卡池概率"),Range(0,1)]
    public float UpDouble;
    [Header("当期五星(金色)角色卡池概率"),Range(0,1)]
    public float CharacterDouble;
    [Header("当期普通角色卡池概率"),Range(0,1)]
    public float HandDouble;

    [Header("Up 角色卡池")] 
    public string characterID;
    
    [Header("五星角色卡池")]
    public List<string> CharacterCradsID = new List<string>();

    [Header("普通角色卡池")]
    public List<string> HandCrads = new List<string>();

    [Header("当期装备卡池")]
    public List<ItemBag> EquipList = new List<ItemBag>();

}
