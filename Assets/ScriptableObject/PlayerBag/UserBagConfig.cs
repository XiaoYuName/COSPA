using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARPG.UI;
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
        
        /// <summary>
        /// 主线任务进度表
        /// </summary>
        public Vector2Int PrincProgress;

     

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
        
        public int MaxExp;
        
        /// <summary>
        /// 好感度经验值
        /// </summary>
        public int Favorabexp;
        
        //--------------装备槽---------------------//
        public EquipHelo[] equipHelos;
        
        /// <summary>
        /// 固定装备类型为六个
        /// </summary>
        public CharacterBag()
        {
            equipHelos = new[]
            {
                new EquipHelo()
                {
                    ItemType = ItemType.武器,
                    item = new Item()
                },
                new EquipHelo()
                {
                    ItemType = ItemType.武器,
                    item = new Item()
                },
                new EquipHelo()
                {
                    ItemType = ItemType.防具, 
                    item = new Item()
                },
                new EquipHelo()
                {
                    ItemType = ItemType.防具,
                    item = new Item()
                },
                new EquipHelo()
                {
                    ItemType = ItemType.首饰,
                    item = new Item()
                },
                new EquipHelo()
                {
                    ItemType = ItemType.首饰,
                    item = new Item()
                },
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
            CharacterState state = InventoryManager.Instance.GetCharacter(ID).State.Clone() as CharacterState;
            for (int i = 0; i < equipHelos.Length; i++)
            {
                if(equipHelos[i].item == null)continue;
                if(equipHelos[i].item.attribute == null)continue;
                for (int E = 0; E < equipHelos[i].item.attribute.Count; E++)
                {
                    switch (equipHelos[i].item.attribute[E].Mode)
                    {
                        case StateMode.物理攻击力:
                            var PhysicsAttackPawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (PhysicsAttackPawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.PhysicsAttack += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.PhysicsAttack += (int)equipHelos[i].item.attribute[E].value*PhysicsAttackPawor;
                            break;
                        case StateMode.魔法攻击力:
                            var MagicAttackPawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (MagicAttackPawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.MagicAttack += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.MagicAttack += (int)equipHelos[i].item.attribute[E].value*MagicAttackPawor;
                            break;
                        case StateMode.生命值:
                            var HPPawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (HPPawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.HP += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.HP += (int)equipHelos[i].item.attribute[E].value*HPPawor;
                            break;
                        case StateMode.生命恢复:
                            var AddHpPawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (AddHpPawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.AddHp += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.AddHp += (int)equipHelos[i].item.attribute[E].value*AddHpPawor;
                            break;
                        case StateMode.物理防御力:
                            var PhysicsDefensePawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (PhysicsDefensePawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.PhysicsDefense += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.PhysicsDefense += (int)equipHelos[i].item.attribute[E].value*PhysicsDefensePawor;
                            break;
                        case StateMode.魔法防御力:
                            var MagicDefensePawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (MagicDefensePawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.MagicDefense += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.MagicDefense += (int)equipHelos[i].item.attribute[E].value*MagicDefensePawor;
                            break;
                        case StateMode.暴击率:
                            state.Cirtical += equipHelos[i].item.attribute[E].value * Mathf.Max(1, equipHelos[i].Powor / 70);
                            break;
                        case StateMode.暴击伤害:
                            state.CirticalAttack += equipHelos[i].item.attribute[E].value * Mathf.Max(1, equipHelos[i].Powor / 70);
                            break;
                        case StateMode.攻击速度:
                            state.AttackSpeed += equipHelos[i].item.attribute[E].value *Mathf.Max(1, equipHelos[i].Powor / 70);
                            break;
                        case StateMode.移动速度:
                            state.MovSpeed += equipHelos[i].item.attribute[E].value *Mathf.Max(1, equipHelos[i].Powor / 70);
                            break;
                        case StateMode.释放速度:
                            state.ReleaseSpeed += (equipHelos[i].item.attribute[E].value *Mathf.Max(1, equipHelos[i].Powor / 70));
                            break;
                        case StateMode.力量:
                            var PowerPawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (PowerPawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.Power += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.Power += (int)equipHelos[i].item.attribute[E].value*PowerPawor;
                            break;
                        case StateMode.智力:
                            var IntelligencePawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (IntelligencePawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.Intelligence += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.Intelligence += (int)equipHelos[i].item.attribute[E].value*IntelligencePawor;
                            break;
                        case StateMode.体力:
                            var VitPawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (VitPawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.Vit += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.Vit += (int)equipHelos[i].item.attribute[E].value*VitPawor;
                            state.HP += (state.Vit/2);
                            break;
                        case StateMode.敏捷:
                            var AgilityPawor = (Mathf.Max(1, equipHelos[i].Powor / 70));
                            if (AgilityPawor < equipHelos[i].Powor) //如果提升率小于装备强化等级，那么装备强化多少级就给多少点的基础属性
                                state.Agility += (int)equipHelos[i].item.attribute[E].value + equipHelos[i].Powor;
                            else
                                state.Agility += (int)equipHelos[i].item.attribute[E].value*AgilityPawor;
                            break;
                        //---不受到武器强化影响-//
                        case StateMode.吸血量:
                            state.Bloodintake += (int)equipHelos[i].item.attribute[E].value;
                            break;
                        case StateMode.技能攻击力:
                            state.SkillAttack += (int)equipHelos[i].item.attribute[E].value;
                            break;
                        default:
                            break;
                    }
                }
            }
            state = Settings.GetLevelGrowthState(Level, state);
            return state;
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
            if (item.level > Level)
            {
                return 1;
            }

           
            var current = equipHelos.ToList();
            //2.1 找到一个没有装备的装备槽位
            var allEquip = current.FindAll(e => e.ItemType == item.Type);
            for (int i = 0; i < current.Count; i++)
            {
                if (String.IsNullOrEmpty(current[i].item.ID) && current[i].ItemType == item.Type) 
                {
                    //1. 装备一个新装备,删除背包的的物品,
                    current[i].item = item;
                    current[i].Powor = bag.power;
                    InventoryManager.Instance.DeleteItemBag(bag,1);
                    equipHelos = current.ToArray();
                    return 3;
                }
            }
            //2. 让玩家选择拆卸掉其中一个
            // UISystem.Instance.ShowPopDialogue("提示","装备栏已满,请选择要替换掉的装备",
            //     allEquip[1].item.ItemName,allEquip[0].item.ItemName,
            //     () =>
            //     {
            //         for (int i = 0; i < current.Count; i++)
            //         {
            //             if (current[i].item == allEquip[1].item)
            //             {
            //                 //拆掉他,
            //                 ItemBag Bag = new ItemBag()
            //                 {
            //                     ID = current[i].item.ID,
            //                     power = current[i].Powor,
            //                     count = 1,
            //                 };
            //                 InventoryManager.Instance.AddItem(Bag);
            //                 //2.1 装备新装备
            //                 current[i].item = InventoryManager.Instance.GetItem(bag.ID);
            //                 current[i].Powor = bag.power;
            //                 equipHelos = current.ToArray();
            //                 InventoryManager.Instance.DeleteItemBag(bag,1);
            //                 UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").PlayCode();
            //                 return;
            //             }
            //         }
            //     }, 
            //     () =>
            //     {
            //         for (int i = 0; i < current.Count; i++)
            //         {
            //             if (current[i].item == allEquip[0].item)
            //             {
            //                 //拆掉他,
            //                 ItemBag Bag = new ItemBag()
            //                 {
            //                     ID = current[i].item.ID,
            //                     power = current[i].Powor,
            //                     count = 1,
            //                 };
            //                 InventoryManager.Instance.AddItem(Bag);
            //                 //2.1 装备新装备
            //                 current[i].item = InventoryManager.Instance.GetItem(bag.ID);
            //                 current[i].Powor = bag.power;
            //                 equipHelos = current.ToArray();
            //                 InventoryManager.Instance.DeleteItemBag(bag,1);
            //                 UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").PlayCode();
            //                 return;
            //             }
            //         }
            //     });
            
            UISystem.Instance.ShowPopDialogue("提示","装备栏已满,请选择要替换掉的装备",
                allEquip[0].item.ItemName,allEquip[1].item.ItemName,
                () =>
                { 
                    for (int i = 0; i < current.Count; i++)
                    {
                        if (current[i].item == allEquip[0].item)
                        {
                            //拆掉他,
                            ItemBag Bag = new ItemBag()
                            {
                                ID = current[i].item.ID,
                                power = current[i].Powor,
                                count = 1,
                            };
                            InventoryManager.Instance.AddItem(Bag);
                            //2.1 装备新装备
                            current[i].item = InventoryManager.Instance.GetItem(bag.ID);
                            current[i].Powor = bag.power;
                            equipHelos = current.ToArray();
                            InventoryManager.Instance.DeleteItemBag(bag,1);
                            UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").PlayCode();
                            return;
                        }
                    }
                }, 
                () =>
                {
                    for (int i = 0; i < current.Count; i++)
                    {
                        if (current[i].item == allEquip[1].item)
                        {
                            //拆掉他,
                            ItemBag Bag = new ItemBag()
                            {
                                ID = current[i].item.ID,
                                power = current[i].Powor,
                                count = 1,
                            };
                            InventoryManager.Instance.AddItem(Bag);
                            //2.1 装备新装备
                            current[i].item = InventoryManager.Instance.GetItem(bag.ID);
                            current[i].Powor = bag.power;
                            equipHelos = current.ToArray();
                            InventoryManager.Instance.DeleteItemBag(bag,1);
                            UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").PlayCode();
                            return;
                        }
                    }
                });
            
            
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

    
    [Serializable]
    public class RewordItemBag
    {
        public ItemBag itemBag;
        public RewordType Type;
    }

    [Serializable]
    public class RandomRewordItemBag
    {
        public ItemBag itemBag;
        public RewordType Type;
        /// <summary>
        /// 概率
        /// </summary>
        [Range(0, 1)] 
        public float random;
    }
}

