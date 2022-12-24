using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Spine.Unity;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// 怪物配置表
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyData",menuName = "ARPG/怪物配置")]
    public class EnemyConfig : Config<EnemyData>
    {
        
    }

    [System.Serializable]
    public class EnemyData : ConfigData
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Header("名字")]
        public string EnemyName;

        /// <summary>
        /// 攻击范围
        /// </summary>
        public float Attackradius;

        [Header("头像")]
        public Sprite icon;

        [Header("类型")]
        public EnemyType Type;
        
        [Header("预制体")]
        public GameObject Prefab;
        
        [Header("自身属性")]
        public CharacterState State;

        [Header("技能")]
        public List<CharacterSkill> SkillTable;

        [Tooltip("复用动画组件")]
        public AnimatorOverrideController Animator;
    }

    public enum EnemyType
    {
        /// <summary>
        /// 普通怪物
        /// </summary>
        Ordinary,
        /// <summary>
        /// 精英怪物
        /// </summary>
        Elite,
        /// <summary>
        /// BOSS怪物
        /// </summary>
        BOSS,
    }
}

