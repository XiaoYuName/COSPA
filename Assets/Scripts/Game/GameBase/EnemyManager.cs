using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public EnemyConfig config;

        protected override void Awake()
        {
            base.Awake();
            config = ConfigManager.LoadConfig<EnemyConfig>("Character/EnemyData");
        }

        /// <summary>
        /// 获取Enemy配置数据
        /// </summary>
        /// <param name="ID">ID</param>
        /// <returns>数据</returns>
        public EnemyData GetData(string ID)
        {
            return config.Get(ID);
        }

        /// <summary>
        /// 获取Enemy 配置预制体
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public GameObject GetPrefab(string ID)
        {
            return GetData(ID).Prefab;
        }
    }
}

