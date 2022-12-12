using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Video;

namespace ARPG.Config
{
    [CreateAssetMenu(fileName = "VideoConfig",menuName = "ARPG/Video")]
    public class VideoConfig : Config<VidoeDataItem>
    {
        
    }

    [System.Serializable]
    public class VidoeDataItem : ConfigData
    {
        [Header("视频源文件")]
        public VideoClip clip;
        
        public bool enableURL;
        
        [EnableIf("enableURL")]
        public string URL;
        
        [Header("描述")]
        public string details;
    }
}

