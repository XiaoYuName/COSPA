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
   
   /// <summary>
   /// 常用UI音效类型
   /// </summary>
   public enum UI_ToolAudio
   {
       /// <summary>
       /// 确认
       /// </summary>
       OnChick,
       /// <summary>
       /// 退出
       /// </summary>
       OutChick,
       /// <summary>
       /// 单击
       /// </summary>
       UI_click,
       /// <summary>
       /// 弹窗
       /// </summary>
       PopWindows,
   }

   public enum FSMType
   {
       /// <summary>
       /// 待机状态
       /// </summary>
       IdleFSM = 0,
       /// <summary>
       /// 巡逻状态
       /// </summary>
       PatrolFSM = 1,
       AttackFSM = 2,
       /// <summary>
       /// 受伤状态
       /// </summary>
       DamageFSM = 9,
   }

   
   /// <summary>
   /// 伤害类型
   /// </summary>
   public enum DamageType
   {
       /// <summary>
       /// 物理伤害
       /// </summary>
       Physics = 1,
       /// <summary>
       /// 魔法伤害
       /// </summary>
       Magic = 2,
       /// <summary>
       /// 治疗伤害
       /// </summary>
       Treatment = 3,
   }

}
