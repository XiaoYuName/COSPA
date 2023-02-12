using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using NaughtyAttributes;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

/// <summary>
/// 扭蛋抽奖界面配置表
/// </summary>
[CreateAssetMenu(fileName = "TwistData",menuName = "ARPG/扭蛋配置")]
public class TwistAnConfig : ScriptableObject
{
    public List<TwistData> TwistDatas = new List<TwistData>();
    
    public List<TwistDouble> Settings = new List<TwistDouble>();


    public TwistData GetTwistData(TwisType _type)
    {
        return TwistDatas.Find(t => t._TwisType == _type);
    }
    
    public TwistDouble GetTwistDouble(TwisType _type)
    {
        return Settings.Find(t => t._TwisType == _type);
    }
}


[System.Serializable]
public class TwistData
{
    [Header("扭蛋类型")]
    public TwisType _TwisType;
    [Header("UP标题内容"),ResizableTextArea]
    public string TitleString;
    [Header("描述"),ResizableTextArea]
    public string description;
    [Header("交换内容描述"),ResizableTextArea]
    public string Helpdescription;

    [Header("限定一次消耗宝石数量")]
    public int SinglentAmount;
    [Header("一次消耗宝石数量")]
    public int OneTwisAmount;
    [Header("十连消耗宝石数量")]
    public int TenTwisAmount;
    
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

    [Header("当期视频播放内容")]
    public MediaReference Video;

    [Header("Up 角色(三星)卡池")] 
    public List<string> characterID;
    
    [Header("二星角色卡池")]
    public List<string> CharacterCradsID = new List<string>();

    [Header("一星角色卡池")]
    public List<string> HandCrads = new List<string>();

    [Header("当期装备卡池")]
    public List<ItemBag> EquipList = new List<ItemBag>();

}
