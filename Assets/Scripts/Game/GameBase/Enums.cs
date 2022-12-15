using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
   public enum CharacterStarType
   {
       三星,
       四星,
       五星,
   }

   /// <summary>
   /// 战斗类型枚举
   /// </summary>
   public enum BattleType
   {
       战士,
       格斗,
       魔法师,
       牧师,
   }

   /// <summary>
    /// 属性
    /// </summary>
   public enum StateMode
   {
       物理攻击力,
       魔法攻击力,
       生命值,
       生命恢复,
       防御力,
       技能攻击力,
       暴击率,
       暴击伤害,
   }


   /// <summary>
   /// 物品类型
   /// </summary>
   public enum ItemType
   {
       武器,
       防具,
       首饰,
       材料,
   }

   /// <summary>
   /// 物品品级
   /// </summary>
   public enum ItemMode
   {
       普通,
       稀有,
       神器,
   }

}
