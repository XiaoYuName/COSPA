using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using NaughtyAttributes;
using RenderHeads.Media.AVProVideo;
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

        [Header("觉醒参数配置")]
        public StepInI[] StepData;
        
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
            StepData = new[]
            {
                new StepInI() {Star = 2, Amount = 99, ItemID = "",Gold = 9999},
                new StepInI() {Star = 3, Amount = 99, ItemID = "",Gold = 9999},
                new StepInI() {Star = 4, Amount = 99, ItemID = "",Gold = 9999},
                new StepInI() {Star = 5, Amount = 99, ItemID = "",Gold = 9999},
                new StepInI() {Star = 6, Amount = 99, ItemID = "",Gold = 9999},
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
        
        [Header("初始默认拥有的BUFF")]
        public List<BuffIDMode> deftualBuffID = new List<BuffIDMode>();

        [Header("扭蛋页所需资源ID")]
        public TwistAssets twistAssets;
        
        [Header("立绘Spine动画")]
        public SkeletonDataAsset TwistSpine;
    }

    [Serializable]
    public class CharacterState:ICloneable
    {
        [Header("物理攻击")]
        public int PhysicsAttack;

        [Header("魔法攻击")]
        public int MagicAttack;

        [Header("物理防御力")]
        public int PhysicsDefense;

        [Header("魔法防御力")]
        public int MagicDefense;

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

        [Header("暴击伤害"),Range(1,200)]
        public float CirticalAttack;

        [Header("力量")]
        public int Power;

        [Header("智力")]
        public int Intelligence;
        
        [Header("体力")]
        public int Vit;
        
        [Header("敏捷")]
        public int Agility;

        [Header("攻击速度")]
        public float AttackSpeed;

        [Header("释放速度")]
        public float ReleaseSpeed;

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
        
        [Tooltip("按下攻击按钮播放的音效,为空则不播放")]
        public string BtnAudioID;
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
        
        [Header("视频Video ID --注: 默认使用AvPro 进行播放")]
        public MediaReference VideAssets;
    }

    [Serializable]
    public class StepInI
    {
        [Tooltip("星级")]
        public int Star;
        [Tooltip("需求材料ID")]
        public string ItemID;
        [Tooltip("需求材料数量")]
        public int Amount;
        [Tooltip("消耗的玛那数量")]
        public int Gold;
    }


    /// <summary>
    /// 扭蛋界面所需资源
    /// </summary>
    [Serializable]
    public class TwistAssets
    {
        /// <summary>
        /// 宣传页VideoID
        /// </summary>
        [Header("宣传页VideoID")]
        public string PropAgAndaVideoID;
        
        [Header("VideoID")]
        public string VideoID;

        [Header("对应视频语音ID")]
        public string AudioHeadID;

        [Header("一,二星角色名字ID")]
        public Sprite NameImage;

        [Header("抽到的界面背景")]
        public Sprite BKImage;

        [Header("Spine动画播放名字")]
        public SpineDialogueAnimation SpineAnimationName;
        
        [Header("Spine Skin 皮肤名称")]
        public SpineDialogueSkin SpineSkinName;
    }
}

