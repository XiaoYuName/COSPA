using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ARPG.Config;
using ARPG.GameSave;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 玩家背包管理器
    /// </summary>
    public class InventoryManager : MonoSingleton<InventoryManager>,ISaveable
    {
        /// <summary>
        /// 玩家背包
        /// </summary>
        private UserBagConfig UserBag;

 

        /// <summary>
        /// 角色配置总表
        /// </summary>
        private CharacterConfig CharacterInfoConfig;

        private BaseItemConfig _itemConfig;
        protected override void Awake()
        {
            base.Awake();
            UserBag = ConfigManager.LoadConfig<UserBagConfig>("PlayerBag/User");
            CharacterInfoConfig = ConfigManager.LoadConfig<CharacterConfig>("Character/Charactern");
            _itemConfig = ConfigManager.LoadConfig<BaseItemConfig>("Character/ItemConfig");
        }

        #region 查

        /// <summary>
        /// 获取角色配置数据
        /// </summary>
        /// <param name="id">唯一ID</param>
        /// <returns>CharacterConfigInfo ： 返回角色的数据</returns>
        /// <exception cref="Exception">如果没有找到则抛出异常</exception>
        public CharacterConfigInfo GetCharacter(string id)
        {
            if (CharacterInfoConfig != null && CharacterInfoConfig.BaseDatas.Any(a => a.ID == id))
            {
                return CharacterInfoConfig.Get(id);
            }
            throw new Exception("角色配置总表未找到对应的角色");
        }

        /// <summary>
        /// 获取玩家所有角色背包
        /// </summary>
        /// <returns></returns>
        public List<CharacterBag> GetAllBag()
        {
            return UserBag.CharacterBags;
        }

        /// <summary>
        /// 获取角色背包
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns></returns>
        public CharacterBag GetBag(string ID)
        {
            return UserBag.CharacterBags.Find(c => c.ID == ID);
        }

        /// <summary>
        /// 获取稀有度对应的Icon
        /// </summary>
        /// <param name="mode">类型</param>
        /// <returns>图片</returns>
        public Sprite GetFaramIcon(ItemMode mode)
        {
            return UserBag.GetFaram(mode);
        }


        /// <summary>
        /// 获取一个Item 配置信息
        /// </summary>
        /// <param name="itemID">item ID</param>
        /// <returns></returns>
        public Item GetItem(string itemID)
        {
            return _itemConfig.Get(itemID);
        }


        /// <summary>
        /// 获取Item 背包内的所有数据
        /// </summary>
        /// <returns></returns>
        public List<ItemBag> GetItemAllBag()
        {
            return UserBag.ItemBags;
        }

        /// <summary>
        /// 获取一条背包数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ItemBag GetItemBag(string ID)
        {
            return UserBag.ItemBags.Find(p => p.ID == ID);
        }

        #endregion

        #region 删

        /// <summary>
        /// 删除背包的的指定道具
        /// </summary>
        /// <param name="itemBag">背包道具</param>
        private void DeleteItemBag(ItemBag itemBag)
        {
            //1.判断背包是否拥有该物品
            if (UserBag.ItemBags.Any(i => i.ID == itemBag.ID))
            {
                UserBag.ItemBags.Remove(itemBag);
                if (isMoney(itemBag.ID))
                {
                    MessageAction.OnUpdataeMoney(GetItemBag(Settings.GemsthoneID)
                        ,GetItemBag(Settings.ManaID));
                }
            }
        }
        
        /// <summary>
        /// 删除背包的的指定道具
        /// </summary>
        /// <param name="itemBag">背包道具</param>
        /// <param name="Amount">数量</param>
        public void DeleteItemBag(ItemBag itemBag,int Amount)
        {
            //1.判断背包是否拥有该物品
            if (UserBag.ItemBags.Any(i => i.ID == itemBag.ID))
            {
                for (int i = 0;  i< UserBag.ItemBags.Count; i++)
                {
                    if (UserBag.ItemBags[i].ID == itemBag.ID)
                    {
                        UserBag.ItemBags[i].count -= Amount;
                        if (UserBag.ItemBags[i].count <= 1)
                        {
                            DeleteItemBag(itemBag);
                            return;
                        }
                        if (isMoney(itemBag.ID))
                        {
                            MessageAction.OnUpdataeMoney(GetItemBag(Settings.GemsthoneID)
                                ,GetItemBag(Settings.ManaID));
                        }
                    }
                }
            }
        }


        #endregion


        #region 增

        /// <summary>
        /// 背包中添加物品
        /// </summary>
        /// <param name="itemBag"></param>
        public void AddItem(ItemBag itemBag)
        {
            if (itemBag == null || itemBag.ID == Settings.ExpID) return;
            //1.如果是货币的话特殊处理
            if (isMoney(itemBag.ID))
            {
                for (int i = 0;  i< UserBag.ItemBags.Count; i++)
                {
                    if (UserBag.ItemBags[i].ID == itemBag.ID)
                    {
                        UserBag.ItemBags[i].count += itemBag.count;
                        return;
                    }
                }
                //2.添加货币后发送刷新事件
                MessageAction.OnUpdataeMoney(GetItemBag(Settings.GemsthoneID)
                    ,GetItemBag(Settings.ManaID));
                return;
            } 


            if (UserBag.ItemBags.Any(i => i.ID == itemBag.ID && i.power == itemBag.power))
            {
                for (int i = 0;  i< UserBag.ItemBags.Count; i++)
                {
                    if (UserBag.ItemBags[i].ID == itemBag.ID)
                    {
                        UserBag.ItemBags[i].count += itemBag.count;
                        return;
                    }
                }
            }
            else
            {
                UserBag.ItemBags.Add(itemBag);
            }
        }

        /// <summary>
        /// 背包中添加物品
        /// </summary>
        /// <param name="item">物品Item</param>
        public void AddItem(Item item)
        {
            ItemBag bag = new ItemBag
            {
                ID = item.ID,
                count = default,
                power = default,
            };
            AddItem(bag);
        }

        #endregion




        /// <summary>
        /// 判断物品ID是否是货币,如果是则进行特殊处理
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private bool isMoney(string ID)
        {
            return ID is Settings.GemsthoneID or Settings.ManaID;
        }

        /// <summary>
        /// 手动刷新下货币
        /// </summary>
        public void UpdateMoney()
        {
            MessageAction.OnUpdataeMoney(GetItemBag(Settings.GemsthoneID)
                ,GetItemBag(Settings.ManaID));
        }


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SaveGameManager.Instance.Save(1);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SaveGameManager.Instance.Load(1);
            }
        }
        
        
        //--SaveGame 游戏存储接口
        public string GUID => "InventoryManager";

        public void Start()
        {
            ISaveable saveable = this;
            saveable.RegisterSaveable();
        }

        public GameSaveData GenerateSaveData()
        {
            GameSaveData data = new GameSaveData();
            JsonTool.SavaGame(UserBag,"Bag");
            return  data;
        }
        public void RestoreData(GameSaveData GameSave)
        {
            UserBag  =  JsonTool.LoadGame<UserBagConfig>("Bag");
        }
        
    }
}

