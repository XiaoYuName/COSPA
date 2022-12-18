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
        /// Icon
        /// </summary>
        [Header("角色界面角色ICON")] 
        public Sprite CharacterPanelIcon;
        /// <summary>
        /// 头像
        /// </summary>
        [Header("头像")]
        public Sprite Headicon;

        [Header("大图立绘")]
        public Sprite OringIcon;
        
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

        /// <summary>
        /// Spine 数据文件
        /// </summary>
        [Header("Spine 动画源文件")]
        public SkeletonDataAsset SpineAsset;
        
        [Header("动画参数")]
        public string SpineIdleName;
        /// <summary>
        /// 装备装备播放动画的动作名称
        /// </summary>
        public string EquipAnimName;
        [Header("角色基础属性")]
        public CharacterState State;

        [Header("故事"),ResizableTextArea]
        public string des;
    }

    [System.Serializable]
    public class CharacterState
    {
        [Header("物理攻击")]
        public int PhysicsAttack;

        [Header("魔法攻击")]
        public int MagicAttack;
        
        [Header("防御力")]
        public int Defense;

        [Header("生命值")]
        public int HP;

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

        [Header("成长系数"),Range(0.1f,100)]
        public float Growth;
    }
}

