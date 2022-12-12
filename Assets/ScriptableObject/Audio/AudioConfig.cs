using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Audio;

namespace ARPG.Audio
{
    [CreateAssetMenu(fileName = "AudioConfig",menuName = "ARPG/Audio/GolabAduioConfig")]
    public class AudioConfig : Config<AudioItem>
    {
        [Header("AudioItem")]
        public GameObject GolodAudioItem;
        
        public List<SceneAudioItem> GameAudio = new List<SceneAudioItem>();
        /// <summary>
        /// 获取场景对应的背景音效名称
        /// </summary>
        /// <param name="scnenName">场景名称</param>
        /// <returns>返回info结构体,内部包含场景所需音效名称</returns>
        public SceneAudioItem GetScneneAudioInfo(string scnenName)
        {
            return GameAudio.FindLast(a => a.SceneName == scnenName);
        }
        
        [Header("AudioMixer Config")]
        public AudioMixer GolodAudioMixer;

        public List<AudioMixerGroupInfo> AudioMixerGroupInfos = new List<AudioMixerGroupInfo>();

        public List<AudioMixerSnapshotsInfo> AudioMixerSnapshotsInfos = new List<AudioMixerSnapshotsInfo>();

        /// <summary>
        /// 获取Audio混合器组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public AudioMixerGroup GetAudioMixerGroup(AudioMixerGroupType type)
        {
            return AudioMixerGroupInfos.FindLast(t => t.Type == type).Group;
        }
        
        /// <summary>
        /// 获取AudioSnapshot组
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public AudioMixerSnapshot GetAudioMixerSnapshot(AudioSnapshotsType type)
        {
            return AudioMixerSnapshotsInfos.FindLast(t => t.Type == type).Snapshot;
        }
    }

    [System.Serializable]
    public class AudioItem : ConfigData
    {
        [Header("音效类型")]
        public AudioType audioType;
        [Header("音效源文件")]
        public AudioClip clip;
        [Header("初始音效大小"),Range(0.1f,1f)]
        public float InitVolume = 1;
        [Header("随机最小音阶"),Range(0.1f,1.5f)]
        public float soundPitchMin = 0.8f;
        [Header("随机最大音阶"),Range(0.1f,1.5f)]
        public float soundPitchMax = 1.2f;
    }

    [System.Serializable]
    public class SceneAudioItem
    {
        [Header("场景名称"),Scene]
        public string SceneName;
        [Header("背景音效名称")]
        public string BgmName;
        [Header("环境音效名称")]
        public string AmbientName;
    }

    [System.Serializable]
    public class AudioMixerGroupInfo
    {
        public AudioMixerGroupType Type;
        public AudioMixerGroup Group;
    }

    [System.Serializable]
    public class AudioMixerSnapshotsInfo
    {
        public AudioSnapshotsType Type;
        public AudioMixerSnapshot Snapshot;
    }

    public enum AudioType
    {
        /// <summary>
        /// 背景音效
        /// </summary>
        BGM,
        /// <summary>
        /// 环境音效
        /// </summary>
        Ambient,
        /// <summary>
        /// 人声音效
        /// </summary>
        Singleton_Head,
        /// <summary>
        /// UI音效
        /// </summary>
        Singleton_UI,
    }

    /// <summary>
    /// Audio混合器混合组类型
    /// </summary>
    public enum AudioMixerGroupType
    {
        /// <summary>
        /// 主混合器
        /// </summary>
        Master,
        /// <summary>
        /// 环境音效主混合器
        /// </summary>
        AmbientMaster,
        /// <summary>
        /// 环境音效混合器
        /// </summary>
        AmbientItem,
        /// <summary>
        /// BGM主混合器
        /// </summary>
        BGMMaster,
        /// <summary>
        /// BGM混合器
        /// </summary>
        BGMItem,
        /// <summary>
        /// UI特效总混合器
        /// </summary>
        UIMaster,
        /// <summary>
        /// UI特效混合器
        /// </summary>
        UIItem,
        /// <summary>
        /// 人声总混合器
        /// </summary>
        HeadMaster,
        /// <summary>
        /// 人声混合器
        /// </summary>
        HeadItem,
    }

    public enum AudioSnapshotsType
    {
        /// <summary>
        /// 默认都有音效
        /// </summary>
        Normal,
        /// <summary>
        /// 仅环境音=> 拉低BGM音效
        /// </summary>
        Ambient,
        /// <summary>
        /// 高人声,低其他音效
        /// </summary>
        Head,
        /// <summary>
        /// 全部停止
        /// </summary>
        Stop,
        /// <summary>
        /// Video 视频音效
        /// </summary>
        Video,
        
    }
}

