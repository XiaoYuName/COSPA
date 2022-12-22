using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// FSM 行为状态
    /// </summary>
    public abstract class FSMBehaviour
    {
        /// <summary>
        /// 初始化Start进入
        /// </summary>
        public abstract void BehaviourStart(Enemy enemy);

        /// <summary>
        /// Update 状态方法
        /// </summary>
        public abstract void BehaviourUpdate(Enemy enemy);
        
        /// <summary>
        /// 退出状态
        /// </summary>
        public abstract void BehaviourEnd(Enemy enemy);

        /// <summary>
        /// 碰撞器触发
        /// </summary>
        /// <param name="other">碰撞者</param>
        /// <param name="enemy">Enemy对象</param>
        public abstract void OnColliderEnter2D(Collision2D other, Enemy enemy);
        
        /// <summary>
        /// 碰撞器退出
        /// </summary>
        /// <param name="other">碰撞者</param>
        /// <param name="enemy">Enemy对象</param>
        public abstract void OnColliderExit2D(Collision2D other, Enemy enemy);
    }
}

