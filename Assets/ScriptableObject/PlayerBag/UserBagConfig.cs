using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Config
{
    /// <summary>
    /// 玩家总表
    /// </summary>
    [CreateAssetMenu(fileName = "User",menuName = "ARPG/User/Bag/UserBag")]
    public class UserBagConfig : ScriptableObject
    {
        /// <summary>
        /// 玩家角色背包
        /// </summary>
        [Header("角色背包")]
        public List<CharacterBag> CharacterBags = new List<CharacterBag>();
    }

    [System.Serializable]
    public class CharacterBag
    {
        /// <summary>
        /// 角色表ID
        /// </summary>
        public string ID;

        /// <summary>
        /// 角色当前星级
        /// </summary>
        public int currentStar;

        /// <summary>
        /// 角色等级
        /// </summary>
        public int Level;
        
        /// <summary>
        /// 角色经验值
        /// </summary>
        public int exp;
        
    }
}

