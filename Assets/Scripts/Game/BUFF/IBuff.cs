using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using Unity.VisualScripting;
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
                    if (BUFFManager.Instance.isNextType(data.buffTrigger))  //判断是否是累计型
                    {
                        if (data.buffPic > 0)
                        {
                            BUFFManager.Instance.AddNextDictionary(tag,data.buffTrigger,this,data.PicMode,data.buffPic);
                        } //判断是直接加伤还是层数加伤
                        else
                        {
                            Debug.LogFormat("添加加成 : {0},值:{1}",data.PicMode,data.maxLevel*data.valueBuff);
                            BUFFManager.Instance.AddNextDictionary(tag,data.buffTrigger,this,data.PicMode,data.maxLevel *
                                data.valueBuff);
                        }

                        switch (data.StopTrigger) //根据BUFF的停止类型进行不同处理
                        {
                            case StopTrigger.持续:
                                BUFFManager.Instance.StartCoroutine(WaitNextPic());
                                break;
                            case StopTrigger.层数清空:
                                BUFFManager.Instance.StartCoroutine(SunLevelWait());
                                break;
                            case StopTrigger.攻击时:
                                break;
                            case StopTrigger.释放技能时:
                                break;
                            case StopTrigger.受击时:
                                break;
                        }
                    }
                    else //如果不是累计类型,则直接进入加伤阶段
                    {
                        BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.PicMode,data.buffPic);
                    }
                    break;
                case BuffType.增益:
                    switch (data.StopTrigger)
                    {
                        case StopTrigger.持续:
                            BUFFManager.Instance.StartCoroutine(WaitCorontiueTime());
                            break;
                        case StopTrigger.层数清空:
                            if (piccorotine != null)
                            {
                                if(curretnLevel<data.maxLevel)
                                    curretnLevel++;
                                //重新计算层数冷却
                                RefBuffUI();
                                BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.PicMode,data.valueBuff*curretnLevel);
                                return;
                            }
                            piccorotine ??= BUFFManager.Instance.StartCoroutine(WaitPicLevel());
                            break;
                        
                        case StopTrigger.攻击时:
                            break;
                        case StopTrigger.释放技能时:
                            break;
                        case StopTrigger.受击时:
                            break;
                        default:
                            break;
                    }
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
                    break;
            }
        }
        //普通层级类型BUFF 持续时间内增加层数
        private IEnumerator WaitPicLevel()
        {
            //1.显示UI,
            curretnLevel = 1;
            AddBuffUI();
            BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.PicMode,data.valueBuff*curretnLevel);
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
                BUFFManager.Instance.AddDictionary(tag,data.buffType,this,data.PicMode,data.valueBuff*curretnLevel);
                if (curretnLevel <= 0)
                {
                    RemoveBuffUI();
                    BUFFManager.Instance.RemoveDictionary(tag,data.buffType,this);
                    piccorotine = null;
                    yield break;
                }
            }
        }

        //持续时间类型
        private IEnumerator WaitNextPic()
        {
            AddBuffUI(false);
            yield return new WaitForSeconds(data.continueTime);
            RemoveBuffUI();
            BUFFManager.Instance.RemoveAddNextDicionary(tag,data.buffTrigger,this);
        }
        
        /// <summary>
        /// 持续时间内获得增益效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitCorontiueTime()
        {
            AddBuffUI(false);
            foreach (var stateValue in data.PropPicList)
            {
                BUFFManager.Instance.AddDictionary(tag,data.buffType,this,stateValue.Mode,stateValue.value);
            }
            yield return new WaitForSeconds(data.continueTime);
            BUFFManager.Instance.RemoveDictionary(tag,data.buffType,this);
            RemoveBuffUI();
        }

        //减层数类型
        private IEnumerator SunLevelWait()
        {
            int lavel = data.maxLevel;
            AddBuffUI();
            for (int i = lavel; i >= 1; i--)
            {
                RefBuffUI(i);
                BUFFManager.Instance.AddNextDictionary(tag,data.buffTrigger,this,data.PicMode,data.valueBuff*i);
                yield return new WaitForSeconds(data.continueTime);
            }
            BUFFManager.Instance.RemoveAddNextDicionary(tag,data.buffTrigger,this);
            RemoveBuffUI();
        }

        /// <summary>
        /// 添加BUFFUI到战斗面板
        /// </summary>
        private void AddBuffUI()
        {
            if(stateUI!= null)
                stateUI.AddBuffItemUI(this);
        }
        
        /// <summary>
        /// 添加BUFFUI到战斗面板
        /// </summary>
        /// <param name="isActiveLevel">是否显示层数</param>
        private void AddBuffUI(bool isActiveLevel)
        {
            if(stateUI!= null)
                stateUI.AddBuffItemUI(this,isActiveLevel);
        }

        /// <summary>
        /// 刷新UI面板的层数显示
        /// </summary>
        private void RefBuffUI()
        {
            if(stateUI!= null)
                stateUI.RefBUFF_UI(this);
        }
        /// <summary>
        /// 刷新UI面板的层数显示
        /// </summary>
        /// <param name="level">层数</param>
        private void RefBuffUI(int level)
        {
            if(stateUI!= null)
                stateUI.RefBUFF_UI(this,level);
        }

        /// <summary>
        /// 移除UI面板的BUFF 显示
        /// </summary>
        private void RemoveBuffUI()
        {
            if(stateUI!= null)
                stateUI.RemoveBUFF_UI(this);
        }
        
    }
}

