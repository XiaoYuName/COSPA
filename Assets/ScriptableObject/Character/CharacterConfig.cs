using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        [Header("角色界面角色ICON")] 
        public Sprite CharacterPanelIcon;
        [Header("头像")]
        public Sprite Headicon;
        [Header("角色星级(最高)")]
        public CharacterStarType CharacterStarType;

        [Header("角色名称")]
        public string CharacterName;

        [Header("职业")]
        public BattleType battle;

    }
}

