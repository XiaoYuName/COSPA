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

    }
}

