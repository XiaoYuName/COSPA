using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Video;

namespace ARPG.Config
{
    /// <summary>
    /// 喵斯快跑活动数据
    /// </summary>
    [CreateAssetMenu(fileName = "DanceConfig",menuName = "ARPG/Activity/Dance")]
    public class DanceConfig : ScriptableObject
    {
        public List<DanceData> _danceDatas = new List<DanceData>();

        public List<DanceCharacterData> _danceCharacter = new List<DanceCharacterData>();
    }
    
    
    /// <summary>
    /// 音乐配置信息
    /// </summary>
    [System.Serializable]
    public class DanceData
    {
        [Header("活动AudioBGM")]
        public string AudioID;
        public Sprite icon;
        [Header("描述信息")]
        public string description;
        [Header("Video")]
        public VideoClip VideoID;
    }

    /// <summary>
    /// 角色配置信息
    /// </summary>
    [System.Serializable]
    public class DanceCharacterData
    {
        [Header("角色名字")]
        public string CharacterName;
        
        [Header("描述")]
        public string description;

        [Header("Spine动画相关配置")]
        public SpineAssets spineAssets;
    }

    [System.Serializable]
    public class SpineAssets
    {
        [Header("主界面选择角色Spine")]
        public SkeletonDataAsset MainShowSpine;
        
        [Header("战斗Spine")]
        public SkeletonDataAsset mainSpine;
        
        [Header("暂未知")]
        public SkeletonDataAsset ghost;

        [Header("战斗失败Spine")]
        public SkeletonDataAsset fail;

        [Header("战场胜利Spine")]
        public SkeletonDataAsset victory;
    }
}

