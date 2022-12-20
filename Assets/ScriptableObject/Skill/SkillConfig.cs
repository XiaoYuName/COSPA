using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    [CreateAssetMenu(fileName = "Skill",menuName = "ARPG/Skill/SkillItem")]
    public class SkillConfig : Config<SkillItem>
    {
        
    }

    [System.Serializable]
    public class SkillItem : ConfigData
    {
        /// <summary>
        /// 技能冷却时间
        /// </summary>
        public float CD;
        /// <summary>
        /// 范围
        /// </summary>
        public float Range;
        /// <summary>
        /// 伤害
        /// </summary>
        public float Diamage;
        /// <summary>
        /// 技能Icon
        /// </summary>
        public Sprite icon;
        /// <summary>
        /// 技能名称
        /// </summary>
        public string SkillName;
        
    }
}

