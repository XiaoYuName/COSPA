using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// 商城标签分页
    /// </summary>
    [CreateAssetMenu(fileName = "商城",menuName = "ARPG/Store/商城设定配置")]
    public class StoreConfig : ScriptableObject
    {
        public List<StoreItem> StoreItems = new List<StoreItem>();

        
        /// <summary>
        /// 获取Type类型的所以配置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<StoreItem> GetTypeStore(StoreType type)
        {
            return StoreItems.FindAll(e => e.Type == type);
        }
    }


    [System.Serializable]
    public class StoreItem
    {
        /// <summary>
        /// 类型
        /// </summary>
        public StoreType Type;
        
        /// <summary>
        /// 消耗RMB
        /// </summary>
        public int RMB;
        
        /// <summary>
        /// 背景
        /// </summary>
        public Sprite BG;

        /// <summary>
        /// 描述
        /// </summary>
        public string description;

        /// <summary>
        /// 奖励数量
        /// </summary>
        public int RewordCount;


    }
    
    

}
