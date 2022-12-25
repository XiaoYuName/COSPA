using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
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

        [Header("地图场景名称"),Scene]
        public string targetScene;

        [Header("该场景的起始点坐标")]
        public Vector3 StarPos;

        [Header("生成怪物设置")]
        
        [Tooltip("列表中每一条代表一波怪物,列表的长度代表该地图的总波数")]
        public List<WaveItem> WaveItems;
    }

    [System.Serializable]
    public class WaveItem
    {
        /// <summary>
        /// 该波数怪物与数量
        /// </summary>
        public List<EnemyBag> EnemyList;

        [Tooltip("每个怪物生成的时间间隔")]
        public float CreateTime;
    }

    /// <summary>
    /// 怪物的背包类，该类是在其他类中调用显示的
    /// </summary>
    [System.Serializable]
    public class EnemyBag
    {
        /// <summary>
        /// EnemyData ID
        /// </summary>
        public string dataID;
        /// <summary>
        /// 数量
        /// </summary>
        public int count;//
        /// <summary>
        /// 出生位置
        /// </summary>
        public Vector3 CreatePos;
        /// <summary>
        /// 随机范围
        /// </summary>
        [Tooltip("随机最大范围")]
        public float MaxRadius;
        [Tooltip("随机最小范围")]
        public float MinRandius;
    }
}

