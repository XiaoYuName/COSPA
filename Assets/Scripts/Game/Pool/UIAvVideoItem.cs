using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Audio;
using ARPG.Config;
using ARPG.UI;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace ARPG.BasePool
{
    public class UIAvVideoItem : UIBase
    {
        private MediaPlayer VideoPlayer;
        private DisplayUGUI VideoImage;

        public override void Init()
        {
            VideoPlayer = GetComponent<MediaPlayer>();
            VideoImage = GetComponent<DisplayUGUI>();
        }
        
        public void StarPlay(AvProItem item)
        {
            VideoPlayer.OpenMedia(item.MediaReference);
            VideoPlayer.Events.AddListener(Call);
            if (!String.IsNullOrEmpty(item.AudioName))
            {
                if (item.SinghtAudio)
                {
                    AudioManager.Instance.PlayAudio(item.AudioName);
                    AudioManager.Instance.SetSnapshot(AudioSnapshotsType.Video,0.5f);
                }
                else
                {
                    AudioManager.Instance.SetSnapshot(AudioSnapshotsType.Video,0.5f);
                }
            }
            VideoImage.transform.localScale = new Vector3(2, 2, 2);
            VideoPlayer.Play();
        }

        private void Call(MediaPlayer player, MediaPlayerEvent.EventType t1, ErrorCode code)
        {
            if (player != VideoPlayer) return;
            if (t1 != MediaPlayerEvent.EventType.FinishedPlaying) return;
            VideoPlayer.Stop();
            AvVideoPool.Instance.Release(this);

            // switch (t1)
            // {
            //     case MediaPlayerEvent.EventType.MetaDataReady:
            //         Debug.Log("视频数据准备完成。当元数据（宽度，持续时间等）可用时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.ReadyToPlay:
            //         Debug.Log("加载视频并准备播放时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.Started:
            //         Debug.Log("播放开始时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.FirstFrameReady:
            //         Debug.Log("渲染第一帧时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.FinishedPlaying:
            //         Debug.Log("当非循环视频播放完毕时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.Closing:
            //         Debug.Log("媒体关闭时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.Error:
            //         Debug.Log("发生错误时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.SubtitleChange:
            //         Debug.Log("字幕更改时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.Stalled:
            //         Debug.Log("媒体停顿/暂停？时触发（例如，当媒体流失去连接时）-当前仅在Windows平台上受支持");
            //         break;
            //     case MediaPlayerEvent.EventType.Unstalled:
            //         Debug.Log("当介质从停止状态恢复时触发（例如，重新建立丢失的连接时）");
            //         break;
            //     case MediaPlayerEvent.EventType.ResolutionChanged:
            //         Debug.Log("当视频的分辨率改变（包括负载）时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.StartedSeeking:
            //         Debug.Log("寻找开始时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.FinishedSeeking:
            //         Debug.Log("搜索完成时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.StartedBuffering:
            //         Debug.Log("缓冲开始时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.FinishedBuffering:
            //         Debug.Log("缓冲完成后触发");
            //         break;
            //     case MediaPlayerEvent.EventType.PropertiesChanged:
            //         Debug.Log("当任何属性（例如，立体声包装改变）时触发-必须手动触发");
            //         break;
            //     case MediaPlayerEvent.EventType.PlaylistItemChanged:
            //         Debug.Log("在播放列表中播放新项目时触发");
            //         break;
            //     case MediaPlayerEvent.EventType.PlaylistFinished:
            //         Debug.Log("播放列表结束时触发");
            //         break;
            // }
        }
    }
    
   
}

