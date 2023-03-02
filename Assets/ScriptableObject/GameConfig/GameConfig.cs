using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// 游戏总默认配置
    /// </summary>
    [CreateAssetMenu(fileName = "GameIni",menuName = "ARPG/Game/默认游戏总设定")]
    public class GameConfig : ScriptableObject
    {
        [Header("稀有度设定")]
        public List<FaramIcon> FaramIcons = new List<FaramIcon>();
        
        /// <summary>
        /// 获取稀有度对应Icon
        /// </summary>
        /// <param name="mode">稀有度</param>
        /// <returns></returns>
        public Sprite GetFaram(ItemMode mode)
        {
            return FaramIcons.Find(f => f.Mode == mode).faram;
        }

        public List<BattleMode> BattleModes = new List<BattleMode>();

        /// <summary>
        /// 获取职业特性描述
        /// </summary>
        /// <param name="_type">职业</param>
        /// <returns></returns>
        public string GetBattleDescription(BattleType _type)
        {
            BattleMode mode = BattleModes.Find(t => t.type == _type);
            if (mode == null)
            {
                return String.Empty;
            }
            return mode.description;
        }

    }
    
    [System.Serializable]
    public class FaramIcon
    {
        /// <summary>
        /// 稀有度
        /// </summary>
        public ItemMode Mode;
        /// <summary>
        /// 边框
        /// </summary>
        public Sprite faram;
    }

    [Serializable]
    public class BattleMode
    {
        public BattleType type;

        [ResizableTextArea]
        public string description;
    }
}
