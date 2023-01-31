using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class IBuff
    {
        /// <summary>
        /// 数据配置
        /// </summary>
        public BuffData data;
        /// <summary>
        /// 当前层数
        /// </summary>
        public int curretnLevel;
        /// <summary>
        /// 释放者
        /// </summary>
        public IBuffLogic tag;

        
        /// <summary>
        /// 触发BUFF Trigger
        /// </summary>
        /// <param name="trigger"></param>
        public void Trigger(BuffTrigger trigger)
        {
            if (data.buffTrigger != trigger) return;
            LogicBehaviour();
        }

        public void LogicBehaviour()
        {
            switch (data.buffType)
            {
                case BuffType.伤害:
                    //TODO： 在进行计算伤害时，计算伤害增加量
                    BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.buffPic);
                    break;
                case BuffType.增益:
                    //TODO: 开始增益自身属性
                    BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.buffPic);
                    break;
                case BuffType.减益:
                    //TODO: 开始减益目标属性
                    break;
                case BuffType.召唤:
                    break;
                case BuffType.治疗:
                    break;
                case BuffType.控制:
                    break;
                default:
                    throw new Exception("未知BUFF类型");
            }
        }
    }
}

