using System.Collections;
using System.Collections.Generic;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace ARPG.Config
{
    [CreateAssetMenu(fileName = "AvVideoConfig",menuName = "ARPG/Video/AvVideo")]
    public class AvVideoConfig : Config<AvProItem>
    {
        
    }

    [System.Serializable]
    public class AvProItem : ConfigData
    {
        [Header("AVPro 桥接文件")]
        public MediaReference MediaReference;
        
        [Header("该视频如果没有自带音效,播放AudioConfig的音效名称")]
        public bool SinghtAudio;
        public string AudioName;
        
        [Header("描述")]
        public string details;
    }
}

