using System.Collections;
using System.Collections.Generic;
using ARPG.Audio;
using UnityEngine;

namespace ARPG.UI.Config
{
    /// <summary>
    /// 用户设置类,用于临时存储用户的本机配置,例如音效等等
    /// </summary>
    [CreateAssetMenu(fileName = "SettringConfig",menuName = "ARPG/Audio/SettringsConfig")]
    public class SettringsConfig : ScriptableObject
    {
        [Header("音效配置")]
        public List<AduioSettrings> AudioSettingsList = new List<AduioSettrings>();

        public float GetGroupTypeValue(AudioMixerGroupType type)
        {
            return AudioSettingsList.Find(a => a.Type == type).Value;
        }

        public void SetGroupTypeVluae(AudioMixerGroupType type,float value)
        {
            AduioSettrings settrings = AudioSettingsList.Find(a => a.Type == type);
            settrings.Value = value;
        }
    }
    
    [System.Serializable]
    public class AduioSettrings
    {
        public AudioMixerGroupType Type;
        [Range(0,1)]
        public float Value;
    }
}

