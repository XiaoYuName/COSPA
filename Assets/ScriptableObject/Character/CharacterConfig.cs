using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using NaughtyAttributes;
using UnityEngine;
using Spine.Unity;

namespace ARPG.Config
{
    /// <summary>
    /// 角色数据配置表
    /// </summary>
    [CreateAssetMenu(fileName = "Charactern",menuName = "ARPG/User/Charactern")]
    public class CharacterConfig : Config<CharacterConfigInfo>
    {
        
    }

    /// <summary>
    /// 角色数据
    /// </summary>
    [System.Serializable]
    public class CharacterConfigInfo : ConfigData
    {
        /// <summary>
        /// 头像
        /// </summary>
        [Header("头像")]
        public Sprite Headicon;
        
        [Header("资源类")]
        public StarSpineAssets[] Assets;
        [Header("角色预制体")]
        public GameObject Prefab;
        
        /// <summary>
        /// 角色星级
        /// </summary>
        [Header("角色星级(最高)")]
        public CharacterStarType CharacterStarType;
        /// <summary>
        /// 角色名称
        /// </summary>
        [Header("角色名称")]
        public string CharacterName;
        /// <summary>
        /// 职业
        /// </summary>
        [Header("职业")]
        public BattleType battle;
        
        [Header("动画参数")]
        public string SpineIdleName;
        /// <summary>
        /// 装备装备播放动画的动作名称
        /// </summary>
        public string EquipAnimName;

        [Header("战斗配置")]
        public AnimatorOverrideController AnimatorController;
        
        [Header("角色基础属性")]
        public CharacterState State;

        [Header("技能配置")]
        public CharacterSkill[] SkillTable;

        public CharacterConfigInfo()
        {
            SkillTable = new[]
            {
                new CharacterSkill() {Type = SkillType.Attack},
                new CharacterSkill() {Type = SkillType.Skill_01},
                new CharacterSkill() {Type = SkillType.Skill_02},
                new CharacterSkill() {Type = SkillType.Skill_03},
                new CharacterSkill(){Type = SkillType.Evolution}
            };
            
        }

        public CharacterSkill GetSkillNameID(SkillType type)
        {
            return SkillTable?.ToList().Find(t => t.Type == type);
        }

        /// <summary>
        /// 获取星级对应资源
        /// </summary>
        /// <param name="Star"></param>
        /// <returns></returns>
        public StarSpineAssets GetAssets(int Star)
        {
            return Star switch
            {
                <3 => Assets[0],
                <6 => Assets[1],
                _ => Assets[2]
            };
        }

        [Header("故事"),ResizableTextArea]
        public string des;
    }

    [Serializable]
    public class CharacterState:ICloneable
    {
        [Header("物理攻击")]
        public int PhysicsAttack;

        [Header("魔法攻击")]
        public int MagicAttack;
        
        [Header("防御力")]
        public int Defense;

        [Header("生命值")]
        public int HP;
        
        [Header("当前生命值")]
        public int currentHp;

        [Header("生命回复")]
        public int AddHp;

        [Header("技能攻击力")]
        public int SkillAttack;

        [Header("暴击几率"),Range(0,1)]
        public float Cirtical;

        [Header("暴击伤害")]
        public int CirticalAttack;

        [Header("力量")]
        public int Power;

        [Header("智力")]
        public int Intelligence;

        [Header("攻击速度")]
        public float AttackSpeed;

        [Header("移动速度")]
        public float MovSpeed;

        [Header("成长系数"),Range(0.1f,100)]
        public float Growth;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    [Serializable]
    public class CharacterSkill
    {
        public string SkillID;
        public SkillType Type;
    }

    public enum SkillType
    {
        /// <summary>
        /// 普通攻击
        /// </summary>
        Attack,
        /// <summary>
        /// 技能1
        /// </summary>
        Skill_01,
        /// <summary>
        /// 技能2
        /// </summary>
        Skill_02,
        /// <summary>
        /// 技能3
        /// </summary>
        Skill_03,
        /// <summary>
        /// 觉醒技能
        /// </summary>
        Evolution
    }

    /// <summary>
    /// 角色星级对应的Spine
    /// </summary>
    [Serializable]
    public class StarSpineAssets
    {
        /// <summary>
        /// 星级
        /// </summary>
        public int Star;
        [Header("大图立绘")]
        public Sprite OringIcon;
        
        [Header("角色界面角色ICON")] 
        public Sprite CharacterPanelIcon;
        
        /// <summary>
        /// 对应的Spine动画
        /// </summary>
        public SkeletonDataAsset Spinedata;
    }
}

