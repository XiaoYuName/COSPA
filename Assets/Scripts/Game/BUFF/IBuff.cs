using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
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
        /// 增益BUFF携程
        /// </summary>
        private Coroutine piccorotine;

        private BuffStateUI stateUI;

        /// <summary>
        /// 触发BUFF Trigger
        /// </summary>
        /// <param name="trigger"></param>
        public void Trigger(BuffTrigger trigger)
        {
            if (data.buffTrigger != trigger) return;
            stateUI = tag.GetStateUI();
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
                    if (piccorotine != null)
                    {
                        if(curretnLevel<data.maxLevel)
                            curretnLevel++;
                        //重新计算层数冷却
                        RefBuffUI();
                        return;
                    }
                    piccorotine ??= BUFFManager.Instance.StartCoroutine(WaitPicLevel());
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

        public IEnumerator WaitPicLevel()
        {
            //1.显示UI,
            curretnLevel = 1;
            AddBuffUI();
            BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.buffPic);
            while (true)
            {
                yield return new WaitForSeconds(data.continueTime); //持续时间内
                if (data.layer == BuffLayer.全部)
                {
                    RemoveBuffUI();
                    BUFFManager.Instance.RemoveDictionary(tag,data.buffType,this);
                    piccorotine = null;
                    yield break;
                }
                curretnLevel--;
                RefBuffUI();
                BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.valueBuff*curretnLevel);
                if (curretnLevel <= 0)
                {
                    RemoveBuffUI();
                    BUFFManager.Instance.RemoveDictionary(tag,data.buffType,this);
                    piccorotine = null;
                    yield break;
                }
            }
        }


        private void AddBuffUI()
        {
            if(stateUI!= null)
                stateUI.AddBuffItemUI(this);
        }

        private void RefBuffUI()
        {
            if(stateUI!= null)
                stateUI.RefBUFF_UI(this);
        }

        private void RemoveBuffUI()
        {
            if(stateUI!= null)
                stateUI.RemoveBUFF_UI(this);
        }
    }
}

