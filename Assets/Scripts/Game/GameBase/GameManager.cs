using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using Cinemachine;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 战斗场景主管理器
    /// </summary>
    public class GameManager : Singleton<GameManager>
    { 
        //角色的公用预制体
        public Character Player;
        private SkillConfig SkillConfig;
        private CinemachineVirtualCamera virtualCamera;

        protected override void Awake()
        {
            base.Awake();
            SkillConfig = ConfigManager.LoadConfig<SkillConfig>("Skill/CharacterSkill");
        }

        public SkillItem GetSkill(string id)
        {
            return SkillConfig.Get(id);
        }


        /// <summary>
        /// 初始化加载战斗场景
        /// </summary>
        /// <param name="bags">玩家列表</param>
        /// <param name="pos">玩家位置</param>
        /// <param name="regionItem">敌人配置</param>
        public IEnumerator StarSceneGame(CharacterBag bags,Vector3 pos,RegionItem regionItem)
        {
            var Obj = GameSystem.Instance.GetPrefab<Character>("Character");
            Player =  Instantiate(Obj, pos, Quaternion.identity);
            Player.Init(bags);
            UISystem.Instance.OpenUI("GaneMemu");
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            virtualCamera.Follow = Player.transform;
            ARPG.Pool.Skill.SkillPoolManager.Instance.Init();
            yield return EnemyManager.Instance.CreateEnemy(regionItem);
        }


        public void QuitGameScene()
        {
            UISystem.Instance.CloseUI("GaneMemu");
            UISystem.Instance.CloseUI("MemuPanel");
            MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
        }




    }

}

