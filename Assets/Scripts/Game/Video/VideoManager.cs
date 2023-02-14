using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using NaughtyAttributes;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.Video;

namespace ARPG.BasePool
{
    public class VideoManager : MonoSingleton<VideoManager>
    {
        private VideoConfig config;
        private AvVideoConfig AvVideoConfig;
        
        protected override void Awake()
        {
            base.Awake();
            config = ConfigManager.LoadConfig<VideoConfig>("Video/VideoConfig");
            AvVideoConfig = ConfigManager.LoadConfig<AvVideoConfig>("Video/AvVideos");
        }
        
        /// <summary>
        /// 在UI上播放视频
        /// </summary>
        /// <param name="videoName">VideoConfig 对应配置元素名称</param>
        public void PlayerVideo(string videoName)
        {
            UIVideoItem videoItem = VideoPool.Instance.Get();
            var item = config.Get(videoName);
            videoItem.Init();
            if(item.enableURL)
                videoItem.StarPlay(item.URL);
            else
                videoItem.StarPlay(item.clip);
        }

        /// <summary>
        /// 在UI上播放AvPro视频
        /// </summary>
        /// <param name="videoID">ID</param>
        public void PlayerAvVideo(string videoID)
        {
            UIAvVideoItem avVideoItem = AvVideoPool.Instance.Get();
            avVideoItem.Init();
            avVideoItem.StarPlay(AvVideoConfig.Get(videoID));
        }

        public MediaReference GetVideo(string ID)
        {
            return AvVideoConfig.Get(ID).MediaReference;
        }

        public VideoClip Get(string ID)
        {
            return config.Get(ID).clip;
        }
    }
}

