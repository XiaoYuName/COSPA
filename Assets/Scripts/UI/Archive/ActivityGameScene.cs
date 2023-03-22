using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 喵斯快跑游戏主场景,主逻辑处理
    /// </summary>
    public class ActivityGameScene : UIBase
    {
        private UGUIVideoPlay VideoPlay;
        private DanceData curretnData;
        public override void Init()
        {
            VideoPlay = Get<UGUIVideoPlay>("Back/BK_VideoUI");
            VideoPlay.Init();
        }

        public void InitData(DanceData data)
        {
            curretnData = data;
            VideoPlay.Play(data.VideoID,false,true,OnGameEnd);
            AudioManager.Instance.PlayAudio(data.AudioID);
        }

        public void CreatPlayer()
        {
            
        }

        /// <summary>
        /// 视频结束,游戏结束
        /// </summary>
        private void OnGameEnd()
        {
            
        }
    } 
}

