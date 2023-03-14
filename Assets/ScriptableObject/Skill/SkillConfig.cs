using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using NaughtyAttributes;
using RenderHeads.Media.AVProVideo;
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
        /// 持续时间
        /// </summary>
        public float Duration;
        /// <summary>
        /// 释放时间
        /// </summary>
        public float ReleaseTime;
        /// <summary>
        /// 范围
        /// </summary>
        [Tooltip("代表技能的范围")]
        public float Radius;
        /// <summary>
        /// 偏移位置
        /// </summary>
        [Tooltip("代表技能的偏移位置")]
        public Vector2 RadiusOffset;
        /// <summary>
        /// 判定层
        /// </summary>
        public LayerMask Mask;
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
        /// <summary>
        /// 技能描述
        /// </summary>
        [ResizableTextArea]
        public string SkillDes;
        [ResizableTextArea]
        public string StepUpDes;
        [Header("解锁星级")]
        public int ActionStar;
        
        /// <summary>
        /// 技能类型
        /// </summary>
        public SkillDamageState SkillType;
        
        public List<ARPG.Pool.Skill.Pool> Pools;

        public List<BuffIDMode> SkillBUFF;
        [Header("技能CG_ID")]
        public MediaReference VideoAsset;
    }

    [System.Serializable]
    public class SkillDamageState
    {
        /// <summary>
        /// 伤害类型
        /// </summary>
        [Tooltip("伤害的类型")]
        public DamageType type;
        [Tooltip("是否是多段伤害的")]
        public bool isMultistage;
        [Tooltip("多段伤害的伤害间隔")]
        public float MultistageTime; 
        [Tooltip("多段伤害的数据： 请注意多段伤害的计算是：首先命中目标后立即执行技能基础伤害,之后每x秒间隔后执行下一段伤害")]
        public List<int> MultistageDamage;
    }
}

