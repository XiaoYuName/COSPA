using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    
    /// <summary>
    /// UI Prefab 预制体Item路径
    /// </summary>
    [CreateAssetMenu(fileName = "UIPrefab",menuName = "ARPG/UI/UIPrefab")]
    public class UIPrefab : Config<UIPrefabItem>
    {
        
    }

    [System.Serializable]
    public class UIPrefabItem : ConfigData
    {
        [Header("预制体")]
        public GameObject Item;
        [Header("描述")]
        public string details;
    }
}

