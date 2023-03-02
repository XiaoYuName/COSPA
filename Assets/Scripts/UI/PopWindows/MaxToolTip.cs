using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
using RenderHeads.Media.AVProVideo;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class MaxToolTip : UIBase
    {
        private Button BackBGBtn;
        private Button VideoBtn;
        private Image BackBG;
        private Image icon;
        private MediaPlayer MediaPlayer;
        private GameObject UIMask;
        
        
        public override void Init()
        {
            BackBGBtn = Get<Button>("UIMask/Maxicon");
            VideoBtn = Get<Button>("UIMask/AVPro Video");
            BackBG = BackBGBtn.GetComponent<Image>();
            icon = Get<Image>("UIMask/Maxicon/HeadIcon");
            MediaPlayer = VideoBtn.GetComponent<MediaPlayer>();
            UIMask = Get("UIMask");
            
            Bind(BackBGBtn,Close,"");
            Bind(VideoBtn,Close,"");
        }

        public void ShowMax(MaxType type,int Star,CharacterConfigInfo info)
        {
            Open();
            switch (type)
            {
                //TODO： icon 功能实现
                case MaxType.icon:
                    break;
                case MaxType.Video:
                    var Clip =  VideoManager.Instance.GetVideo(info.GetAssets(Star).VideoID);
                    if (Clip == null) Close();
                    MediaPlayer.gameObject.SetActive(true);
                    MediaPlayer.OpenMedia(Clip);
                    MediaPlayer.Loop = true;
                    break;
            }
        }

        public override void Close()
        {
            UIMask.gameObject.SetActive(false);
            if (MediaPlayer.gameObject.activeSelf)
            {
                MediaPlayer.Stop();
                MediaPlayer.gameObject.SetActive(false);
            }

            if (BackBG.gameObject.activeSelf)
            {
                BackBG.gameObject.SetActive(false);
            }
        }

        public override void Open()
        {
            UIMask.gameObject.SetActive(true);
        }
    }

    public enum MaxType
    {
        icon,
        Video,
    }

}
