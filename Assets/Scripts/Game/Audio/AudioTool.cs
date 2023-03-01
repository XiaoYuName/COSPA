using System;
using UnityEngine;

namespace ARPG.Audio
{
    /// <summary>
    /// 播放框架音乐小组件
    /// </summary>
    public class AudioTool : MonoBehaviour
    {
        [Header("参数设置")]
        public string AudioID;
        public bool isLoop;
        public AudioType Type;

        public void OnEnable()
        {
            if (Type == AudioType.Video)
            {
                AudioManager.Instance.PlayVideoLoop(AudioID);
            }
            else
            {
                AudioManager.Instance.PlayAudio(AudioID);
            }

            
        }
        public void OnDisable()
        {
            if (Type == AudioType.Video)
            {
                if(AudioManager.IsInitialized)
                    AudioManager.Instance.StopVideoLoop();
            }
        }
    }
}

