using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.UI.Config
{
    /// <summary>
    /// 剧情配置表
    /// </summary>
    [CreateAssetMenu(fileName = "主线剧情",menuName = "ARPG/主线剧情")]
    public class PrincipalLineConfig : ScriptableObject
    {
        /// <summary>
        /// 所有的章节
        /// </summary>
        public List<PrincLine> MianPrincipalLineList = new List<PrincLine>();

    }

    /// <summary>
    /// 章节
    /// </summary>
    [System.Serializable]
    public class PrincLine
    {
        [Header("章节名称")]
        public string PrincName;
        
        /// <summary>
        /// 剧情进度列表
        /// </summary>
        public List<PrincItem> PrincItemList = new List<PrincItem>();
    }

    /// <summary>
    /// 剧情
    /// </summary>
    [System.Serializable]
    public class PrincItem
    {
        [Header("章节名称")]
        public string PrincItemName;
        [Header("跳转剧情场景名称"),Scene]
        public string SceneName;
    }

}
