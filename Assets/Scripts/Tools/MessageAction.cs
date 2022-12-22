using System;
using ARPG.Config;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG
{
    public static  class MessageAction
    {
        #region 场景切换

        public static event Action<string, Vector3,CharacterBag,RegionItem> StartGameScene;

        /// <summary>
        /// 首次进入战斗场景
        /// </summary>
        /// <param name="scneneName">场景名称</param>
        /// <param name="pos">生成位置</param>
        /// <param name="Character">玩家配置</param>
        /// <param name="regionItem">敌人配置</param>
        public static void OnStartGameScene(string scneneName, Vector3 pos,CharacterBag Character,RegionItem regionItem)
        {
            StartGameScene?.Invoke(scneneName,pos,Character,regionItem);
        }

        public static event Action<string, Vector3> TransitionEvent;
        
        public static void OnTransitionEvent(string name, Vector3 pos)
        {
            TransitionEvent?.Invoke(name, pos);
        }
        
        /// <summary>
        /// 场景卸载前回调
        /// </summary>
        public static event Action BeforScenenUnloadEvent;
        /// <summary>
        /// 场景卸载前回调
        /// </summary>
        public static void OnBeforScenenUnloadEvent()
        {
            BeforScenenUnloadEvent?.Invoke();
        }
        
        /// <summary>
        /// 新场景加载后回调
        /// </summary>
        public static event Action AfterScenenLoadEvent;
        /// <summary>
        /// 新场景加载后回调
        /// </summary>
        public static void OnAfterScenenLoadEvent()
        {
            AfterScenenLoadEvent?.Invoke();
        }
        
        /// <summary>
        /// 移动玩家坐标
        /// </summary>
        public static event Action<Vector3> MovToPosint;

        ///移动玩家坐标
        public static void OnMovToPosint(Vector3 obj)
        {
            MovToPosint?.Invoke(obj);
        }
        
        #endregion
        
    }
}

