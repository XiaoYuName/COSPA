using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace ARPG.UI.Config
{
    [CreateAssetMenu(fileName = "副本",menuName = "ARPG/副本")]
    public class RegionConfig : ScriptableObject
    {
        /// <summary>
        /// 所有的地区
        /// </summary>
        public List<RegionLine> RegionList = new List<RegionLine>();
    }
    
    [System.Serializable]
    public class RegionLine
    {
        [Header("章节名称")]
        public string RegionName;

       
        public Sprite backIcon;
        
        /// <summary>
        /// 剧情进度列表
        /// </summary>
        public List<RegionItem> RegionItemList = new List<RegionItem>();
    }
    /// <summary>
    /// 地图
    /// </summary>
    [System.Serializable]
    public class RegionItem
    {
        [Header("副本名称orID")]
        public string RegionItemName;
        [Header("icon")]
        public Sprite backIcon;
    }
}

