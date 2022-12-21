using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
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
        /// <param name="pos">玩家初始位置</param>
        /// <param name="enemyDatas">怪物组</param>
        public IEnumerator StarSceneGame(CharacterBag[] bags,Vector3 pos,EnemyData[] enemyDatas)
        {
            for (int i = 0; i < bags.Length; i++)
            {
                yield return StarSceneGame(bags[i], pos);
            }
            yield return null;
            //TODO: 加载怪物预制体
        }
        
        /// <summary>
        /// 初始化加载战斗场景
        /// </summary>
        /// <param name="bags">玩家列表</param>
        /// <param name="pos">玩家位置</param>
        public IEnumerator StarSceneGame(CharacterBag bags,Vector3 pos)
        {
            var Obj = GameSystem.Instance.GetPrefab<Character>("Character");
            Player =  Instantiate(Obj, pos, Quaternion.identity);
            Player.Init(bags);
            UISystem.Instance.OpenUI("GaneMemu");
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            virtualCamera.Follow = Player.transform;
            yield return new WaitForSeconds(0);
        }


        public void QuitGameScene()
        {
            UISystem.Instance.CloseUI("GaneMemu");
            UISystem.Instance.CloseUI("MemuPanel");
            MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
        }




    }

}

