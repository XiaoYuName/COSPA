using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 战斗场景工场
    /// </summary>
    public class GameSystem : MonoSingleton<GameSystem>
    {
        /// <summary>
        /// Prefab 配置表
        /// </summary>
        private UIPrefab PrefabConfig;

        /// <summary>
        /// 副本奖励配置表
        /// </summary>
        private MapConfig MapConfig;
        /// <summary>
        /// 技能配置表
        /// </summary>
        private SkillConfig SkillConfig;

        /// <summary>
        /// 精灵配置图集
        /// </summary>
        private SpriteConfig SpriteConfig;

        /// <summary>
        /// 游戏设定配置
        /// </summary>
        private GameConfig GameConfig;

        /// <summary>
        /// 玩法帮助文本配置
        /// </summary>
        private HelpConfig helpConfig;

        protected override void Awake()
        {
            base.Awake();
            Init();
            Application.targetFrameRate = 120;
        }
        
        private void Init()
        {
            PrefabConfig = ConfigManager.LoadConfig<UIPrefab>("UIPrefab/GamePrefab");
            MapConfig = ConfigManager.LoadConfig<MapConfig>("Map/MapData");
            SkillConfig = ConfigManager.LoadConfig<SkillConfig>("Skill/CharacterSkill");
            SpriteConfig = ConfigManager.LoadConfig<SpriteConfig>("Character/SpriteConfig");
            GameConfig = ConfigManager.LoadConfig<GameConfig>("GameIni/GameIni");
            helpConfig = ConfigManager.LoadConfig<HelpConfig>("HelpText/HelpTextConfig");
        }

        /// <summary>
        /// 获取技能配置数据
        /// </summary>
        /// <param name="id">技能ID</param>
        /// <returns></returns>
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
        /// 获取精灵
        /// </summary>
        /// <param name="id">配置表ID</param>
        /// <returns></returns>
        public Sprite GetSprite(string id)
        {
            return SpriteConfig.Get(id).Sprite;
        }

        /// <summary>
        /// 获取稀有度框
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Sprite GetFaram(ItemMode mode)
        {
            return GameConfig.GetFaram(mode);
        }

        /// <summary>
        /// 获取说明配置文本
        /// </summary>
        /// <param name="helpType">说明类型</param>
        /// <returns></returns>
        public HelpTextItem GetHelpItem(HelpType helpType)
        {
            return helpConfig.Get(helpType);
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


        /// <summary>
        /// 设置游戏FPS
        /// </summary>
        /// <param name="value"></param>
        public void SetFPS(int value)
        {
            Application.targetFrameRate = value;
        }


    }
    
}

