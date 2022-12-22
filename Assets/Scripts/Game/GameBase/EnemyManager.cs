using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ARPG.Config;
using ARPG.UI.Config;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ARPG
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        public EnemyConfig config;
        private RegionItem currentRegion;
        private List<Enemy> CurrenEnemys;
        private int currentIndex; //当前生产Enemy的波数

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

        /// <summary>
        /// 预加载Enemy 预制体
        /// </summary>
        /// <param name="data">副本数据配置</param>
        /// <returns></returns>
        public IEnumerator CreateEnemy(RegionItem data)
        {
            //1. 先把所有的怪物预加载上去
            currentRegion = data;
            List<Pool.Skill.Pool> CreatBag = new List<Pool.Skill.Pool>();
            
            for (int i = 0; i < data.WaveItems.Count; i++)  //循环每波敌人
            {
                foreach (var enemyBag in data.WaveItems[i].EnemyList) //循环每个波中的所有敌人
                {
                    GameObject Prefab = GetPrefab(enemyBag.dataID);
                    if (CreatBag.Any(p => p.prefab == Prefab))  //如果有相同的敌人,只需要增加数量即可，不用重复生成相同的预制体
                    {
                        for (int j = 0; j < CreatBag.Count; j++)
                        {
                            if (CreatBag[j].prefab == Prefab)
                            {
                                CreatBag[j].count += enemyBag.count;
                            }
                        }
                    }
                    else
                    {
                        CreatBag.Add(new Pool.Skill.Pool{prefab = Prefab,count = enemyBag.count});
                    }
                }
                yield return null;
            }

            foreach (var Pool in CreatBag)
            {
                EnemyPoolManager.Instance.AddPoolPrefab(Pool);
                yield return null;
            }
            EnemyPoolManager.Instance.Init();
        }
        
        
        public void PlayEnemy()
        {
            //如果加载好的新场景是当前数据的目标场景，那么就表示场景加载已完毕，开始准备刷怪
            currentIndex = 0;
            UISystem.Instance.DownTime(5,InstanceEnemy);
        }

        /// <summary>
        /// 实例化加载Enemy 开始进入状态机状态
        /// </summary>
        protected void InstanceEnemy()
        {
            //1.首先生成第一波怪物
            CurrenEnemys = new List<Enemy>();
            for (int i = 0; i < currentRegion.WaveItems[currentIndex].EnemyList.Count; i++)
            {
                EnemyBag data = currentRegion.WaveItems[currentIndex].EnemyList[i];
                GameObject Prefab = GetPrefab(data.dataID);
                Vector3 pos = data.CreatePos;
                int RandomX = (int)Random.Range(-data.Radius, data.Radius);
                int RandomY = (int)Random.Range(-data.Radius, data.Radius);
                Enemy enemy =  EnemyPoolManager.Release(Prefab, pos + new Vector3(RandomX, RandomY, 0), Quaternion.identity).GetComponent<Enemy>();
                enemy.Init(GetData(data.dataID));
                CurrenEnemys.Add(enemy);
            }
            StartCoroutine(WaitCuurrentEnemyDead());
        }
        
        /// <summary>
        /// 等待当前列表内怪物全部死亡，开始进行下一波怪物调用
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitCuurrentEnemyDead()
        {
            while (CurrenEnemys is { Count: >= 1 })
            {
                yield return null;
            }
            InstanceEnemy();
        }

        //TODO: 使用异步编程实现
        // protected async void WaitEnemyDead()
        // {
        //     await Task.Run(delegate
        //     {
        //         while (CurrenEnemys is { Count: >= 1 })
        //         {
        //             Thread.Sleep(1000);
        //         }
        //     });
        //     
        //     
        // }
    }
}

