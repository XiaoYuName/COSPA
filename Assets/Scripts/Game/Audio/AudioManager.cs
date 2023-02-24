using System;
using System.Collections;
using ARPG.Audio;
using ARPG.Audio.Item;
using ARPG.GameSave;
using ARPG.UI.Config;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using AudioType = ARPG.Audio.AudioType;
using Random = UnityEngine.Random;

namespace ARPG
{
    public class AudioManager : MonoSingleton<AudioManager>,ISaveable
    {
        private AudioConfig MainAudioData;
        private SettringsConfig audioSettrings;
        /// <summary>
        /// 背景音效
        /// </summary>
        private AudioSource BGMAudio;
        /// <summary>
        /// 环境音效
        /// </summary>
        private AudioSource AmbientAudio;
        /// <summary>
        /// 不叠加单音效声音
        /// </summary>
        private AudioSource HeadAudio;

        /// <summary>
        /// 视频音效
        /// </summary>
        private AudioSource VideoAudio;

        /// <summary>
        /// 切换场景后,背景音效开始播放的随机时间
        /// </summary>
        private int AudioSwitchRadom => Random.Range(3, 5);
        private float SpanshotTime = 3f;
        private Coroutine AduioCoroutine;
        #region MixerCompontnt
        private AudioMixer Mixer;
        private AudioMixerSnapshot normalSnapshot;
        private AudioMixerSnapshot AmbientSnapshot;

        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            MainAudioData = AudioConfig.GetConfig<AudioConfig>("Audio/AudioConfig");
            audioSettrings = ConfigManager.LoadConfig<SettringsConfig>("Audio/SettringConfig");
            BGMAudio = transform.Find("GameBGM").GetComponent<AudioSource>();
            AmbientAudio = transform.Find("GameAmbient").GetComponent<AudioSource>();
            HeadAudio =transform.Find("GameHead").GetComponent<AudioSource>();//GameHead
            VideoAudio = transform.Find("VideoAudio").GetComponent<AudioSource>();
            VideoAudio.gameObject.SetActive(false);
            Mixer = MainAudioData.GolodAudioMixer;
            normalSnapshot = MainAudioData.GetAudioMixerSnapshot(AudioSnapshotsType.Normal);
            AmbientSnapshot = MainAudioData.GetAudioMixerSnapshot(AudioSnapshotsType.Ambient);
        }

        private void OnEnable()
        {
            MessageAction.AfterScenenLoadEvent += OpenNewScene;
        }
        private void OnDisable()
        {
            MessageAction.AfterScenenLoadEvent -= OpenNewScene;
        }
        
        //加载场景回调
        public void OpenNewScene()
        {
            string currentScnenName = SceneManager.GetActiveScene().name;
            SceneAudioItem scneneAudioInfo = MainAudioData.GetScneneAudioInfo(currentScnenName);
            if (scneneAudioInfo == null) return;
            AudioItem bgmItem = MainAudioData.Get(scneneAudioInfo.BgmName);
            AudioItem Ambient = MainAudioData.Get(scneneAudioInfo.AmbientName);
           
            if(AduioCoroutine != null)
                StopCoroutine(AduioCoroutine); //先停止之前的协程
            AduioCoroutine = StartCoroutine(PlaySceneAudio(bgmItem, Ambient));
        }

        /// <summary>
        /// 播放场景音效
        /// </summary>
        /// <param name="BgmItem"></param>
        /// <param name="AmbientItem"></param>
        /// <returns></returns>
        private IEnumerator PlaySceneAudio(AudioItem BgmItem,AudioItem AmbientItem)
        {
            if (BgmItem != null && AmbientItem != null)
            {
                PlayAmbient(AmbientItem,1f);
                yield return new WaitForSeconds(AudioSwitchRadom);
                PlayBGM(BgmItem,SpanshotTime);
            }
            else
            {
                Debug.LogWarning("该场景未配置音效文件");
            }
        }

        /// <summary>
        /// 播放背景音效
        /// </summary>
        /// <param name="item"></param>
        /// <param name="SnapshotTime">过度时间</param>
        private void PlayBGM(AudioItem item,float SnapshotTime)
        {
            if (item == null || item.clip == null) return;
            BGMAudio.clip = item.clip;
            BGMAudio.volume = item.InitVolume;
            if (BGMAudio.isActiveAndEnabled)
            {
                BGMAudio.Play();
            }
            normalSnapshot.TransitionTo(SnapshotTime);
        }

        /// <summary>
        /// 播放环境音效
        /// </summary>
        /// <param name="item"></param>
        /// <param name="SnapshotTime">过度时间</param>
        private void PlayAmbient(AudioItem item,float SnapshotTime)
        {
            if (item == null || item.clip == null) return;
            AmbientAudio.clip = item.clip;
            AmbientAudio.volume = item.InitVolume;
            if (AmbientAudio.isActiveAndEnabled)
            {
                AmbientAudio.Play();
            }
            AmbientSnapshot.TransitionTo(SnapshotTime);
        }
        
        /// <summary>
        /// 播放一个循环播放的视频音效
        /// </summary>
        /// <param name="AudioId"></param>
        public void PlayVideoLoop(string AudioId)
        {
            AudioItem ItemInfo = MainAudioData.GetVideoAudio(AudioId);
            VideoAudio.gameObject.SetActive(true);
            VideoAudio.clip = ItemInfo.clip;
            SetSnapshot(AudioSnapshotsType.Video,0.5f);
            VideoAudio.loop = true;
            VideoAudio.Play();
        }

        /// <summary>
        /// 停止循环播放的视频音效
        /// </summary>
        public void StopVideoLoop()
        {
            VideoAudio.Stop();
            VideoAudio.gameObject.SetActive(false);
            VideoAudio.clip = null;
            SetSnapshot(AudioSnapshotsType.Normal,0.5f);
        }
        

        /// <summary>
        /// 播放一个音效
        /// </summary>
        /// <param name="AudioID">音效名称</param>
        /// <exception cref="Exception">音效类型</exception>
        public void PlayAudio(string AudioID)
        {
            AudioItem ItemInfo = MainAudioData.Get(AudioID);
            switch (ItemInfo.audioType)
            {
                case AudioType.BGM:
                    PlayBGM(ItemInfo, 0.25f);
                    return;
                case AudioType.Ambient:
                    PlayAmbient(ItemInfo,1);
                    return;
                case AudioType.Singleton_Head:
                    SetSnapshot(AudioSnapshotsType.Head, 1);
                    break;
                case AudioType.Singleton_UI:
                    break;
                case AudioType.Video :
                    SetSnapshot(AudioSnapshotsType.Video, 1);
                    break;
                default:
                    throw new Exception("没有对应Switch 的类型音效");
            }
            AudioGame audioGame =  PoolManager.Release(MainAudioData.GolodAudioItem).GetComponent<AudioGame>(); 
            audioGame.AudioSource.clip = ItemInfo.clip;
            audioGame.AudioSource.outputAudioMixerGroup = ItemInfo.audioType switch
            {
               AudioType.BGM => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.BGMItem),
               AudioType.Ambient => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.AmbientItem),
               AudioType.Singleton_UI => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.UIItem),
               AudioType.Singleton_Head => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.HeadItem),
               AudioType.Video => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.VideoItem),
               _=> MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.Master)
            };
            audioGame.aduioID = AudioID;
            audioGame.AudioSource.volume = ItemInfo.InitVolume;
            audioGame.Play();
        }
        
        
        
        /// <summary>
        /// 播放一个音效
        /// </summary>
        /// <param name="AudioID">音效ID</param>
        /// <param name="func">播放完毕回调函数(注:不适用于BGM与环境音)</param>
        /// <exception cref="Exception">未被包裹的类型,将抛出异常</exception>
        public void PlayAudio(string AudioID,Action func)
        { 
            AudioItem ItemInfo = MainAudioData.Get(AudioID);
            switch (ItemInfo.audioType)
            {
                case AudioType.BGM:
                    PlayBGM(ItemInfo, 3);
                    return;
                case AudioType.Ambient:
                    PlayAmbient(ItemInfo,1);
                    return;
                case AudioType.Singleton_Head:
                    SetSnapshot(AudioSnapshotsType.Head, 1);
                    break;
                case AudioType.Singleton_UI:
                    break;
                default:
                    throw new Exception("没有对应Switch 的类型音效");
            }
            AudioGame audioGame =  PoolManager.Release(MainAudioData.GolodAudioItem).GetComponent<AudioGame>(); 
            audioGame.AudioSource.clip = ItemInfo.clip;
            audioGame.AudioSource.outputAudioMixerGroup = ItemInfo.audioType switch
            {
                AudioType.BGM => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.BGMItem),
                AudioType.Ambient => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.AmbientItem),
                AudioType.Singleton_UI => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.UIItem),
                AudioType.Singleton_Head => MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.HeadItem),
                _=> MainAudioData.GetAudioMixerGroup(AudioMixerGroupType.Master)
            };
            audioGame.aduioID = AudioID;
            audioGame.AudioSource.volume = ItemInfo.InitVolume;
            audioGame.AudioSource.pitch = Random.Range(ItemInfo.soundPitchMin, ItemInfo.soundPitchMax);
            audioGame.Play(func);
        }

        /// <summary>
        /// 播放一个不叠加音效,如果音效播放器正在播放,会停止之前的,播放新的
        /// </summary>
        /// <param name="AudioID">音效ID</param>
        public void PlayHeadAudio(string AudioID)
        {
            AudioItem ItemInfo = MainAudioData.Get(AudioID);
            HeadAudio.clip = ItemInfo.clip;
            HeadAudio.volume = ItemInfo.InitVolume;
            HeadAudio.pitch = Random.Range(ItemInfo.soundPitchMin, ItemInfo.soundPitchMax);
            HeadAudio.Play();
        }

        


        /// <summary>
        /// 设置混合器过度
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="time">过度时间</param>
        public void SetSnapshot(AudioSnapshotsType type,float time)
        {
            AudioMixerSnapshot mixerSnapshot = MainAudioData.GetAudioMixerSnapshot(type);
            mixerSnapshot.TransitionTo(time);
        }
        
        /// <summary>
        /// 设置混合组音效
        /// </summary>
        /// <param name="type">混合组类型</param>
        /// <param name="volme">音效大小 范围0~1</param>
        public void SetAudioTypeVolme(AudioMixerGroupType type,float volme)
        {
            switch (type)
            {
                case AudioMixerGroupType.Master:
                    SetMixerVolme("MasterVolume", volme);
                    break;
                case AudioMixerGroupType.AmbientMaster:
                    SetMixerVolme("AmbientMaster", volme);
                    break;
                case AudioMixerGroupType.AmbientItem:
                    SetMixerVolme("AmbientVolume", volme);
                    break;
                case AudioMixerGroupType.BGMMaster:
                    SetMixerVolme("BGMMaster", volme);
                    break;
                case AudioMixerGroupType.BGMItem:
                    SetMixerVolme("BGMVolume", volme);
                    break;
                case AudioMixerGroupType.UIMaster:
                    SetMixerVolme("UIMaster", volme);
                    break;
                case AudioMixerGroupType.UIItem:
                    SetMixerVolme("UIVolume", volme);
                    break;
                case AudioMixerGroupType.HeadMaster:
                    SetMixerVolme("HeadMaster", volme);
                    break;
                case AudioMixerGroupType.HeadItem:
                    SetMixerVolme("HeadVolme", volme);
                    break;
                case AudioMixerGroupType.VideoMaster:
                    SetMixerVolme("VideoMaster", volme);
                    break;
                case AudioMixerGroupType.VideoItem:
                    SetMixerVolme("VideoItem", volme);
                    break;
            }
            //保存数据
            audioSettrings.SetGroupTypeVluae(type,volme);
        }

        /// <summary>
        /// 恢复被禁用的音效
        /// </summary>
        /// <param name="type">音效值</param>
        /// <returns></returns>
        public float OpenAudioTypeVolme(AudioMixerGroupType type)
        {
            float value = audioSettrings.GetGroupTypeValue(type);
            if (type == AudioMixerGroupType.Master)
                value = 0.8f;
            SetAudioTypeVolme(type,value);
            return value;
        }

        private void  SetMixerVolme(string MixerName, float value)
        {
            Mixer.SetFloat(MixerName, ConvertMixerVolme(value));
        }

        /// <summary>
        /// 将0~1的音效转换为音阶值
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        private float ConvertMixerVolme(float amount)
        {
            return (amount * 100 - 80);
        }
        

        public float GetMaskValue(AudioMixerGroupType _type)
        {
            return audioSettrings.GetGroupTypeValue(_type);
        }

        //--------------------------------保存接口---------------------------//
        public string GUID => "AudioManager";
        
        public void Start()
        {
            ISaveable saveable = this;
            saveable.RegisterSaveable();
        }
        
        public GameSaveData GenerateSaveData()
        {
            GameSaveData saveData = new GameSaveData();
            JsonTool.SavaGame(audioSettrings,GUID+"AudioSettings.save");
            return saveData;
        }

        public void RestoreData(GameSaveData GameSave)
        {
            audioSettrings = JsonTool.LoadGame<SettringsConfig>(GUID + "AudioSettings.save");
        }
    }
}

