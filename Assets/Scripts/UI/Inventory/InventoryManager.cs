using System.Collections;
using System.Collections.Generic;
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

        protected override void Awake()
        {
            base.Awake();
            UserBag = ConfigManager.LoadConfig<UserBagConfig>("");
        }
    }
}

