using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG
{
    public class VideoManager : Singleton<VideoManager>
    {
        private VideoConfig config;
        
        protected override void Awake()
        {
            base.Awake();
            config = ConfigManager.LoadConfig<VideoConfig>("Video/VideoConfig");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                PlayerVideo("PV_BH3");
            }
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
    }
}

