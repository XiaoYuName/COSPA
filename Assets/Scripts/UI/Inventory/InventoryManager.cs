using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 玩家背包管理器
    /// </summary>
    public class InventoryManager : Singleton<InventoryManager>
    {
        /// <summary>
        /// 玩家背包
        /// </summary>
        private UserBagConfig UserBag;
        /// <summary>
        /// 角色配置总表
        /// </summary>
        private CharacterConfig CharacterInfoConfig;

        protected override void Awake()
        {
            base.Awake();
            UserBag = ConfigManager.LoadConfig<UserBagConfig>("PlayerBag/User");
            CharacterInfoConfig = ConfigManager.LoadConfig<CharacterConfig>("Character/Charactern");
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
        /// 获取玩家角色背包
        /// </summary>
        /// <returns></returns>
        public List<CharacterBag> GetBag()
        {
            return UserBag.CharacterBags;
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

        #endregion

        
        
    }
}

