using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// 玩家总表
    /// </summary>
    [CreateAssetMenu(fileName = "User",menuName = "ARPG/User/Bag/UserBag")]
    public class UserBagConfig : ScriptableObject
    {
        /// <summary>
        /// 玩家角色背包
        /// </summary>
        [Header("角色背包")]
        public List<CharacterBag> CharacterBags = new List<CharacterBag>();

        /// <summary>
        /// 玩家背包道具
        /// </summary>
        [Header("背包道具Item")]
        public List<ItemBag> ItemBags = new List<ItemBag>();
    }

    [System.Serializable]
    public class CharacterBag
    {
        /// <summary>
        /// 角色表ID
        /// </summary>
        public string ID;

        /// <summary>
        /// 角色当前星级
        /// </summary>
        public int currentStar;

        /// <summary>
        /// 角色等级
        /// </summary>
        public int Level;
        
        /// <summary>
        /// 角色经验值
        /// </summary>
        public int exp;
        
        //--------------装备槽---------------------//
        public EquipHelo[] equipHelos;
        
        /// <summary>
        /// 固定装备类型为六个
        /// </summary>
        public CharacterBag()
        {
            equipHelos = new[]
            {
                new EquipHelo() { ItemType = ItemType.武器, },
                new EquipHelo() { ItemType = ItemType.武器, },
                new EquipHelo() { ItemType = ItemType.防具, },
                new EquipHelo() { ItemType = ItemType.防具, },
                new EquipHelo() { ItemType = ItemType.首饰, },
                new EquipHelo() { ItemType = ItemType.首饰, },
            };
        }
    }

    /// <summary>
    /// 物品背包
    /// </summary>
    [System.Serializable]
    public class ItemBag
    {
        /// <summary>
        /// 物品ID
        /// </summary>
        public string ID;
        
        /// <summary>
        /// 物品数量
        /// </summary>
        public int count;
    }


    /// <summary>
    /// 装备槽类
    /// </summary>
    [System.Serializable]
    public class EquipHelo
    {
        /// <summary>
        /// 装备类型
        /// </summary>
        public ItemType ItemType;
        
        /// <summary>
        /// 装备
        /// </summary>
        public Item item;
    }
    

}

