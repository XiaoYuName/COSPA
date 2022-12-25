using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 战斗场景工场
    /// </summary>
    public class GameSystem : Singleton<GameSystem>
    {
        /// <summary>
        /// Prefab 配置表
        /// </summary>
        private UIPrefab PrefabConfig;

        /// <summary>
        /// 副本奖励配置表
        /// </summary>
        private MapConfig MapConfig;
        
        private SkillConfig SkillConfig;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }
        
        private void Init()
        {
            PrefabConfig = ConfigManager.LoadConfig<UIPrefab>("UIPrefab/GamePrefab");
            MapConfig = ConfigManager.LoadConfig<MapConfig>("Map/MapData");
            SkillConfig = ConfigManager.LoadConfig<SkillConfig>("Skill/CharacterSkill");
        }

        public SkillItem GetSkill(string id)
        {
            return SkillConfig.Get(id);
        }
        
        /// <summary>
        /// 获取单个地图副本的奖励配置
        /// </summary>
        /// <param name="id">副本ID</param>
        /// <returns></returns>
        public MapItem GetMapReword(string id)
        {
            return MapConfig.Get(id);
        }

        
        /// <summary>
        /// 获取一个预制体GameObject 对象
        /// </summary>
        /// <param name="uiname">对象名称</param>
        /// <returns>返回GameObject对象</returns>
        /// <exception cref="Exception">如果找不到,则抛出异常</exception>
        public GameObject GetPrefab(string uiname)
        {
            GameObject Obj = PrefabConfig.Get(uiname).Item;
            if (Obj == null)
            {
                throw new Exception("GamePrefab表中没有找到 :" + uiname);
            }
            return Obj;
        }

        /// <summary>
        /// 获取一个预制体GameObject对象上的组件
        /// </summary>
        /// <param name="uiname">对象名称</param>
        /// <typeparam name="T">对象类型,该类型必须继承自Component</typeparam>
        /// <returns>返回类型对象</returns>
        /// <exception cref="Exception">如果找不到,则抛出异常</exception>
        public T GetPrefab<T>(string uiname)
        {
            GameObject Obj = GetPrefab(uiname);
            T Tobj = Obj.GetComponent<T>();
            if (Tobj == null)
            {
                throw new Exception("GamePrefab表中没有对应脚本对象 :" + uiname);
            }
            return Tobj;
        }
        
        /// <summary>
        /// 生成一个Game对象
        /// </summary>
        /// <param name="Gamename">名称</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public T InstanceGame<T>(string Gamename) where T: MonoBehaviour
        { 
            T Obj = GetPrefab<T>(Gamename);
            var ui = Instantiate(Obj);
            return ui;
        }
        
        /// <summary>
        /// 生成一个Game对象
        /// </summary>
        /// <param name="Gamename">名称</param>
        /// <param name="parent">父级对象</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>返回加载好的对象</returns>
        public T InstanceGame<T>(string Gamename,Transform parent)where T: MonoBehaviour
        { 
            T Obj = GetPrefab<T>(Gamename);
            var ui = Instantiate(Obj.gameObject, parent);
            return ui.GetComponent<T>();
        }
        
        /// <summary>
        /// 生成一个Game对象
        /// </summary>
        /// <param name="Gamename">名称</param>
        /// <param name="pos">位置</param>
        /// <param name="rotation">旋转</param>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>返回加载好的对象</returns>
        public T InstanceGame<T>(string Gamename,Vector3 pos,Quaternion rotation) where T: MonoBehaviour
        { 
            T Obj = GetPrefab<T>(Gamename);
            var ui = Instantiate(Obj, pos, rotation);
            return ui;
        }
    }
    
}

