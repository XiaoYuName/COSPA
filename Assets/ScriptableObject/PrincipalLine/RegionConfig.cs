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
        
        /// <summary>
        /// 独立副本
        /// </summary>
        public List<RegionItem> RegionSingleton = new List<RegionItem>();


        /// <summary>
        /// 根据副本名称获取独立副本配置
        /// </summary>
        /// <param name="RegionItemName">副本名称</param>
        /// <returns></returns>
        public RegionItem GetRegionSingleton(string RegionItemName)
        {
            return RegionSingleton.Find(n => n.RegionItemName == RegionItemName);
        }
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

        [Tooltip("主线章节进度")]
        public Vector2Int Press;

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

        [Header("BUFF 设定")]
        
        [Tooltip("本波是否有BUFF")]
        public bool isOpenBuff;
        /// <summary>
        /// BUFF 
        /// </summary>
        public RegionBuffData BuffList;
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

    /// <summary>
    /// 波数BUFF列表
    /// </summary>
    [System.Serializable]
    public class RegionBuffData
    {
        [Header("BUFF选择器")]
        public List<string> Buff_ID = new List<string>();

        [Tooltip("表示当前波中可以选择几个BUFF")]
        public int count;
    }
}

