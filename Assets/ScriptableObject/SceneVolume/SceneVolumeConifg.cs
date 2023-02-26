using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace ARPG
{
    [CreateAssetMenu(fileName = "SceneVolume",menuName = "ARPG/VolumeConfig")]
    public class SceneVolumeConifg : ScriptableObject
    {
        public List<SceneVolumeConfig> SceneProFile = new List<SceneVolumeConfig>();


        /// <summary>
        /// 获取当前激活的场景后处理文件
        /// </summary>
        /// <returns></returns>
        public VolumeProfile GetActiveSceneVolumeProfile()
        {
            string SceneName = SceneManager.GetActiveScene().name;
            return SceneProFile.Find(n => n.SceneName == SceneName).volumeProfile;
        }
    }

    [System.Serializable]
    public class SceneVolumeConfig
    {
        [Scene]
        public string SceneName;

        [Header("后处理文件")]
        public VolumeProfile volumeProfile;
    }
}

