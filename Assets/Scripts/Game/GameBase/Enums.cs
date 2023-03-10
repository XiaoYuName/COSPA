namespace ARPG
{
   public enum CharacterStarType
   {
       一星=1,
       二星=2,
       三星=3,
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
       魅魔,
   }

   /// <summary>
    /// 属性
    /// </summary>
   public enum StateMode
   {
       物理攻击力=0,
       魔法攻击力=1,
       生命值=2,
       生命恢复=3,
       防御力=4,
       物理防御力=5,
       魔法防御力=6,
       技能攻击力=7,
       暴击率=8,
       暴击伤害=9,
       攻击速度=10,
       移动速度=11,
       最终伤害=12,
       释放速度=13,
       治疗量=14,
       吸血量=15,
       力量=16,
       智力=17,
       体力=18,
       敏捷=19,
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
       记忆碎片,
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
   

   public enum FSMType
   {
       /// <summary>
       /// 空状态机
       /// </summary>
       Note = 99,
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
       /// <summary>
       /// 死亡状态
       /// </summary>
       DieFSM = 10,
       /// <summary>
       /// BOSS特殊状态
       /// </summary>
       BOSSBehaviour=11,
       /// <summary>
       /// BOSS攻击状态
       /// </summary>
       BOSSAttackFSM=12,
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


   /// <summary>
   /// 奖励类型
   /// </summary>
   public enum RewordType
   {
       /// <summary>
       /// 空
       /// </summary>
       Not,
       /// <summary>
       /// BOSS奖励
       /// </summary>
       BOSS,
       /// <summary>
       /// 首次通关奖励
       /// </summary>
       Star,
       /// <summary>
       /// 三星奖励
       /// </summary>
       ThreeStar,
       /// <summary>
       /// 随机概率获得
       /// </summary>
       Random,
   }

   public enum UITableType
   {
       /// <summary>
       /// 低UI宽度自适应层
       /// </summary>
       UIDonw,
       /// <summary>
       /// UI高度自适应层
       /// </summary>
       UIRoot,
       /// <summary>
       /// 高UI宽度自适应层
       /// </summary>
       UITop,
       /// <summary>
       /// 高UI高度自适应层
       /// </summary>
       TopUIRoot,
       /// <summary>
       /// 宽度顶层
       /// </summary>
       AutoTop,
   }


   /// <summary>
   /// 货币类型
   /// </summary>
   public enum GoldType
   {
       玛那,
       宝石,
   }


   public enum SpineDialogueSkin
   {
       not,
       anger,
       joy,
       normal,
       sad,
       shy,
       special_a,
       surprised,
   }

   public enum SpineDialogueAnimation
   {
       Not,
       eye_blink,
       eye_close,
       eye_idle,
       eye_open,
       mouth_close,
       mouth_idle,
       mouth_oepn,
       mouth_talk,
       pseudo_setup_pose,
   }

   public enum DialogueFarmeMode
   {
       /// <summary>
       /// 黄色边框
       /// </summary>
       DialogueYellow,
       /// <summary>
       /// 红色边框
       /// </summary>
       DialogueRed,
       /// <summary>
       /// 蓝色边框
       /// </summary>
       DialogueBule,
   }


   public enum C2C
   {
       EventMsg = 1,
       BOSSEventMsg=2,
       ItemBagAmount=3,
   }

   /// <summary>
   /// 商城分页Type
   /// </summary>
   public enum StoreType
   {
       宝石,
       玛娜,
       道具,
   }

   public enum BuffType
   {
       伤害,
       增益,
       减益,
       召唤,
       治疗,
       控制
       
   }

   public enum BuffTrigger
   {
       攻击时,
       释放技能时,
       受击时,
       死亡时,
       攻击领主时,
       移动时,
       站立时,
       回复自身时,
       累计攻击,
       累计技能,
       累计受击,
       累计移动,
   }

   public enum StopTrigger
   {
       持续,
       层数清空,
       攻击时,
       释放技能时,
       受击时,
   }

   public enum EndTrigger
   {
       Not,
       攻击时,
       释放技能时,
       受击时,
       回复自身时,
       攻击领主时,
       移动时,
   }

   public enum BuffLayer
   {
       单层,
       全部,
   }

   public enum BuffBehaviourType
   {
       光环,
       瞬时,
   }


   public enum BuffIDMode
   {
       无我,
       剑气,
       急速,
       快速施法,
       气韵,
       超越,
       领悟,
       觉悟,
       顿悟,
       气势,
       猫猫凯露,
       屠龙者,
       灭龙者,
       猎龙者,
   }

   public enum IDType
   {
       物品,
       怪物,
       角色,
   }
   
   public enum TaskTrigger
   {
       击杀怪物,
       击杀BOSS,
       在线,
       获得角色,
       获得道具,
       通关地下城,
       充值,
       释放技能,
       扭蛋,
   }

   public enum TaskMode
   {
       共享,
       日常,
       探索,
       活动,
       限定,
   }

   public enum TaskTableMode
   {
       每日,
       普通,
       限定,
       称号,
   }

   public enum TaskState
   {
       未完成=0,
       待领取=2,
       已领取=3,
   }

   public enum TaskRefType
   {
       不刷新=0,
       每天刷新=1,
       每小时刷新=2,
       每月刷新=3,
   }

   public enum TwisType
   {
       白金扭蛋,
       PILCK_UP,
       普通扭蛋,
   }

   public enum TwistMode
   {
       限定一次,
       单次,
       十连,
   }

   public enum NoticeType
   {
       活动信息=0,
       更新日志=1,
       BUG信息=2,
   }

   public enum NoticeMode
   {
       扭蛋=0,
       资讯=1,
       运营=2,
       告知=3,
   }

   public enum MemuTableMode
   {
       属性=0,
       故事=1,
       简介=2,
   }

   public enum EquipTableType
   {
       通常=0,
       地下城=1,
       深渊=2,
       女神的秘石=3,
       限定=4,
   }

   /// <summary>
   /// 回调的时机
   /// </summary>
   public enum FuncMode
   {
       /// <summary>
       /// 一开始
       /// </summary>
       Star=0,
       /// <summary>
       /// 中途
       /// </summary>
       Crent=1,
       /// <summary>
       /// 结束
       /// </summary>
       End=2,
   }

   public enum RegionMode
   {
       普通=0,
       困难=1,
       地狱=2,
       噩梦=3,
   }

   public enum LookState
   {
       未开启=0,
       已解锁=1,
       已通关=2,
   }

   public enum RegionRandomType
   {
       None,
       深渊_1,
       深渊_2,
       深渊_3,
       深渊_4,
       深渊_5,
   }


   public enum GameResult
   {
       胜利,
       失败,
       中途退出,
   }
}
