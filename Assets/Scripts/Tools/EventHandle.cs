using System;
using UnityEngine;

namespace ARPG.Global
{
    public static  class EventHandle
    {
        #region 场景切换

        public static event Action<string, Vector3> StartGameScene;

        /// <summary>
        /// 首次进入战斗场景
        /// </summary>
        /// <param name="scneneName"></param>
        /// <param name="pos"></param>
        public static void OnStartGaneScene(string scneneName, Vector3 pos)
        {
            StartGameScene?.Invoke(scneneName,pos);
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

