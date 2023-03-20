using System.Collections;
using System.Collections.Generic;
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
    }
    
    
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
}

