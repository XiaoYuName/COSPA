using System;
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

        [Header("稀有度设定")]
        public List<FaramIcon> FaramIcons = new List<FaramIcon>();

        /// <summary>
        /// 获取稀有度对应Icon
        /// </summary>
        /// <param name="mode">稀有度</param>
        /// <returns></returns>
        public Sprite GetFaram(ItemMode mode)
        {
            return FaramIcons.Find(f => f.Mode == mode).faram;
        }
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
        /// 好感度
        /// </summary>
        public int Favorability;

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

        /// <summary>
        /// 当前角色属性
        /// </summary>
        public CharacterState CurrentCharacterState => Operation();

        /// <summary>
        /// 计算当前属性状态
        /// </summary>
        /// <returns></returns>
        public CharacterState Operation()
        {
            //TODO: 暂时测试返回默认基础属性
            return InventoryManager.Instance.GetCharacter(ID).State;
        }


       /// <summary>
       /// 装备装备
       /// </summary>
       /// <param name="bag">装备</param>
       /// <returns>返回Code 值: 1 等级不足  2 : 没有找到对应的装备槽，3：装备成果</returns>
        public int SetEquipHelo(ItemBag bag)
       {
           Item item = InventoryManager.Instance.GetItem(bag.ID);
            
            //1.首先判断该装备能否装备，条件是否满足
            if (item.level >= Level)
            {
                return 1;
            }
            
            //2.判断当前是否已经装备了装备,如果已经装备了，将装备了的装备拆下，讲该装备装备上去
            for (int i = 0; i < equipHelos.Length; i++)
            {
                if (item.Type == equipHelos[i].ItemType)
                {
                    if (String.IsNullOrEmpty(equipHelos[i].item.ID))
                    {
                        //1. 装备一个新装备,删除背包的的物品,
                        equipHelos[i].item = item;
                        equipHelos[i].Powor = bag.power;
                        InventoryManager.Instance.DeleteItemBag(bag,1);
                    }
                    else
                    {
                        //2.拆下旧装备
                        ItemBag Bag = new ItemBag()
                        {
                            ID = equipHelos[i].item.ID,
                            power = equipHelos[i].Powor,
                            count = 1,
                        };
                        InventoryManager.Instance.AddItem(Bag);
                        //2.1 装备新装备
                        equipHelos[i].item = item;
                        equipHelos[i].Powor = bag.power;
                        InventoryManager.Instance.DeleteItemBag(bag,1);
                    }
                    return 3;
                }
            }
            //3.通知刷新
            return 2;

           
            
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

        public int power;
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

        /// <summary>
        /// 强化等级
        /// </summary>
        public int Powor;
    }


    [System.Serializable]
    public class FaramIcon
    {
        /// <summary>
        /// 稀有度
        /// </summary>
        public ItemMode Mode;
        /// <summary>
        /// 边框
        /// </summary>
        public Sprite faram;
    }
    


}

