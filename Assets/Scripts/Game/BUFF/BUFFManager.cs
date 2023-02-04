using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        //释放者---触发类型-触发BUFF--加成值
        private Dictionary<IBuffLogic, Dictionary<BuffTrigger, Dictionary<IBuff, Dictionary<StateMode, float>>>>
            CurrentNextBuff = new Dictionary<IBuffLogic, Dictionary<BuffTrigger, Dictionary<IBuff, Dictionary<StateMode, float>>>>();

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
            float res = GetTypeDeftualValue(Mode);
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

        
        /// <summary>
        /// 获取Trigger 类型的默认加成值
        /// </summary>
        /// <param name="trigger">触发类型</param>
        /// <returns>默认加成值</returns>
        public float GetTypeDeftualValue(BuffTrigger trigger)
        {
            return trigger switch
            {
                BuffTrigger.累计受击 => 1,
                BuffTrigger.累计技能 => 1,
                BuffTrigger.累计攻击 => 1,
                _ => 0,
            };
        }

        public float GetTypeDeftualValue(StateMode mode)
        {
            return mode switch
            {
                StateMode.技能攻击力 => 0,
                StateMode.攻击速度 => 0,
                StateMode.暴击伤害 => 0,
                StateMode.暴击率 => 0,
                StateMode.最终伤害 => 0,
                StateMode.物理攻击力 => 0,
                StateMode.生命值 => 0,
                StateMode.生命恢复 => 0,
                StateMode.移动速度 => 1,
                StateMode.释放速度 => 0,
                StateMode.防御力 => 0,
                StateMode.魔法攻击力 => 0,
                _ => 1,
            };
        }


        //--------------------------累计增益BUFF字典-------------------------------//
        public void AddNextDictionary(IBuffLogic character, BuffTrigger trigger, IBuff buff, StateMode mode,
            float value)
        {
            if (!CurrentNextBuff.ContainsKey(character))
            {
                CurrentNextBuff.Add(character,new Dictionary<BuffTrigger, Dictionary<IBuff, Dictionary<StateMode, float>>>());
            }
            if (!CurrentNextBuff[character].ContainsKey(trigger))
            {
                CurrentNextBuff[character].Add(trigger,new Dictionary<IBuff, Dictionary<StateMode, float>>());
            }
            if (!CurrentNextBuff[character][trigger].ContainsKey(buff))
            {
                CurrentNextBuff[character][trigger].Add(buff,new Dictionary<StateMode, float>());
            }
            if (!CurrentNextBuff[character][trigger][buff].ContainsKey(mode))
            {
                CurrentNextBuff[character][trigger][buff].Add(mode,value);
            }
            CurrentNextBuff[character][trigger][buff][mode] = value;
        }

        /// <summary>
        /// 获取累计类型BUFF加成
        /// </summary>
        /// <param name="character">释放者</param>
        /// <param name="trigger">触发类型</param>
        /// <param name="mode">加成类型</param>
        /// <returns>加成value</returns>
        public float GetNextDicTypeValue(IBuffLogic character,BuffTrigger trigger,StateMode mode)
        {
            float res = 0;
            if (CurrentNextBuff.ContainsKey(character))
            {
                if (CurrentNextBuff[character].ContainsKey(trigger))
                {
                    for (int i = 0; i < CurrentNextBuff[character][trigger].Count; i++)
                    {
                        (IBuff data, Dictionary<StateMode, float> Key) = CurrentNextBuff[character][trigger].ElementAt(i);

                        if (Key.ContainsKey(mode))
                        {
                            for (int j = 0; j < Key.Count; j++)
                            {
                                (StateMode State, float value) = Key.ElementAt(j);
                                if (State == mode)
                                {
                                    res += value;
                                    if (data.data.StopTrigger == StopTrigger.释放技能时)
                                    {
                                        CurrentNextBuff[character][trigger].Remove(data);
                                        character.GetStateUI().RemoveBUFF_UI(data);
                                    }
                                }
                            }
                        }
                    }
                }

            }

            if (res == 0)
                res = GetTypeDeftualValue(mode);
            return res;
        }

        /// <summary>
        /// 移除单个BUFF加成
        /// </summary>
        /// <param name="character">角色</param>
        /// <param name="trigger">触发类型</param>
        /// <param name="buff">IBUFF 实例</param>
        public void RemoveAddNextDicionary(IBuffLogic character, BuffTrigger trigger, IBuff buff)
        {
            if (CurrentNextBuff.ContainsKey(character))
            {
                if (CurrentNextBuff[character].ContainsKey(trigger))
                {
                    if (CurrentNextBuff[character][trigger].ContainsKey(buff))
                    {
                        CurrentNextBuff[character][trigger].Remove(buff);
                    }
                }
            }
        }
    }
}

