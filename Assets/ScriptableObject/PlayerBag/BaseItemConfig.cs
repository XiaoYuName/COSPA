using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// 物品总表单
    /// </summary>
    [CreateAssetMenu(fileName = "ItemConfig",menuName = "ARPG/User/ItemConfig")]
    public class BaseItemConfig : Config<Item>
    {
    }
    
    [System.Serializable]
    public class Item : ConfigData
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string ItemName;

        /// <summary>
        /// 要求穿戴等级
        /// </summary>
        public int level;
        /// <summary>
        /// 图标ID 
        /// </summary>
        public string spriteID;
        
        /// <summary>
        /// Item 类型
        /// </summary>
        public ItemType Type;

        /// <summary>
        /// 物品品质
        /// </summary>
        public ItemMode Mode;
        
        /// <summary>
        /// 属性
        /// </summary>
        public List<StateValue> attribute;

        /// <summary>
        /// 描述
        /// </summary>
        [TextArea]
        public string description;

        /// <summary>
        /// 出售价格
        /// </summary>
        public int sellAmount;

        /// <summary>
        /// 购买价格
        /// </summary>
        public int purchase;

        /// <summary>
        /// 是否可交易
        /// </summary>
        public bool isSell;
    }

    [System.Serializable]
    public class StateValue
    {
        public StateMode Mode;
        public int value;
    }
    
}

