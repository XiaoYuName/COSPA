using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
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
        


        /// <summary>
        /// 初始化加载战斗场景
        /// </summary>
        /// <param name="bags">玩家列表</param>
        /// <param name="LoadSceneName">加载的场景名称</param>
        /// <param name="enemyDatas">怪物组</param>
        public void StarSceneGame(CharacterBag[] bags,string LoadSceneName,EnemyData[] enemyDatas)
        {
            
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
            yield return new WaitForSeconds(0);
        }



        
    }

}

