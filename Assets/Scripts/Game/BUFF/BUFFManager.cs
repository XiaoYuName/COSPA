using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class BUFFManager : MonoSingleton<BUFFManager>
    {
        /// <summary>
        /// 当前BUFF增益字典
        /// </summary>
        private Dictionary<IBuffLogic, Dictionary<BuffType, Dictionary<IBuff, Dictionary<StateMode,float>>>> CurretnBUFF =
            new Dictionary<IBuffLogic, Dictionary<BuffType, Dictionary<IBuff, Dictionary<StateMode,float>>>>();
        
        
        /// <summary>
        /// 注册BUFF增益字典表
        /// </summary>
        /// <param name="character">释放者</param>
        /// <param name="type">BUFF类型</param>
        /// <param name="Buff">BUFF技能</param>
        /// <param name="BUFFMode">加成类型</param>
        /// <param name="value">增益值</param>
        public void AddDictionary(IBuffLogic character,BuffType type,IBuff Buff,StateMode BUFFMode,float value)
        {
            if (!CurretnBUFF.ContainsKey(character))
                CurretnBUFF.Add(character,new Dictionary<BuffType, Dictionary<IBuff, Dictionary<StateMode, float>>>());
            if (!CurretnBUFF[character].ContainsKey(type))
                CurretnBUFF[character].Add(type,new Dictionary<IBuff, Dictionary<StateMode, float>>());
            if(!CurretnBUFF[character][type].ContainsKey(Buff))
                CurretnBUFF[character][type].Add(Buff,new Dictionary<StateMode, float>());
            if(!CurretnBUFF[character][type][Buff].ContainsKey(BUFFMode))
                CurretnBUFF[character][type][Buff].Add(BUFFMode,value);
            CurretnBUFF[character][type][Buff][BUFFMode] = value;
            
        }

        /// <summary>
        /// 取消注册全部增益
        /// </summary>
        /// <param name="character"></param>
        public void RemoveDictionary(IBuffLogic character)
        {
            if (CurretnBUFF.ContainsKey(character))
                CurretnBUFF.Remove(character);
        } 

        /// <summary>
        /// 取消注册单个类型增益
        /// </summary>
        /// <param name="character">角色</param>
        /// <param name="type">Buff类型</param>
        public void RemoveDictionary(IBuffLogic character, BuffType type)
        {
            if (CurretnBUFF.ContainsKey(character))
            {
                if (CurretnBUFF[character].ContainsKey(type))
                {
                    CurretnBUFF[character].Remove(type);
                }
            }
        }
        
        /// <summary>
        /// 取消注册单个BUFF增益
        /// </summary>
        /// <param name="character">角色</param>
        /// <param name="type">类型</param>
        /// <param name="IBuff">BUFF</param>
        public void RemoveDictionary(IBuffLogic character, BuffType type,IBuff IBuff)
        {
            if (CurretnBUFF.ContainsKey(character))
            {
                if (CurretnBUFF[character].ContainsKey(type))
                {
                    if (CurretnBUFF[character][type].ContainsKey(IBuff))
                    {
                        CurretnBUFF[character][type].Remove(IBuff);
                    }
                }
            }
        }

        /// <summary>
        /// 获取角色某一类型BUFF加成
        /// </summary>
        /// <param name="character">角色</param>
        /// <param name="type">类型</param>
        /// <param name="Mode">修改的状态类型</param>
        public float GetTyepValue(IBuffLogic character, BuffType type,StateMode Mode)
        {
            float res = type == BuffType.伤害?1:0;
            if (CurretnBUFF.ContainsKey(character))
            {
                if (CurretnBUFF[character].ContainsKey(type))
                {
                    foreach (var KeyBUFF in CurretnBUFF[character][type])
                    {
                        if (CurretnBUFF[character][type].ContainsKey(KeyBUFF.Key))
                        {
                            if(CurretnBUFF[character][type][KeyBUFF.Key].ContainsKey(Mode))
                                res += CurretnBUFF[character][type][KeyBUFF.Key][Mode];
                        }
                    }
                }
            }
            return res;
        }

        public bool isNextType(BuffTrigger trigger)
        {
            if (trigger == BuffTrigger.累计攻击 || trigger == BuffTrigger.累计技能 || trigger == BuffTrigger.累计受击)
                return true;
            return false;
        }
    }
}

