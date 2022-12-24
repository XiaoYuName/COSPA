using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.Pool.Skill;
using ARPG.UI;
using ARPG.UI.Config;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ARPG
{
    /// <summary>
    /// 战斗场景主管理器
    /// </summary>
    public class GameManager : Singleton<GameManager>
    { 
        //角色的公用预制体
        public Character Player;
        private SkillConfig SkillConfig;
        private CinemachineVirtualCamera virtualCamera;
        private GameObject DamageWordUI;
        /// <summary>
        /// 是否在战斗场景
        /// </summary>
        [HideInInspector]public bool isGameScnen;

        protected override void Awake()
        {
            base.Awake();
            SkillConfig = ConfigManager.LoadConfig<SkillConfig>("Skill/CharacterSkill");
            DamageWordUI = GameSystem.Instance.GetPrefab("DamageText");
        }

        public SkillItem GetSkill(string id)
        {
            return SkillConfig.Get(id);
        }


        /// <summary>
        /// 初始化加载战斗场景
        /// </summary>
        /// <param name="bags">玩家列表</param>
        /// <param name="pos">玩家位置</param>
        /// <param name="regionItem">敌人配置</param>
        public IEnumerator StarSceneGame(CharacterBag bags,Vector3 pos,RegionItem regionItem)
        {
            var Obj = GameSystem.Instance.GetPrefab<Character>("Character");
            Player =  Instantiate(Obj, pos, Quaternion.identity);
            Player.Init(bags);
            UISystem.Instance.OpenUI("GaneMemu");
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            virtualCamera.Follow = Player.transform;
            isGameScnen = true;
            ARPG.Pool.Skill.SkillPoolManager.Instance.AddPoolPrefab(new Pool.Skill.Pool
            {
                prefab = DamageWordUI,
                count =  100,
            });
            yield return EnemyManager.Instance.CreateEnemy(regionItem);
            ARPG.Pool.Skill.SkillPoolManager.Instance.Init();
        }


        public void QuitGameScene()
        {
            isGameScnen = false;
            UISystem.Instance.CloseUI("GaneMemu");
            UISystem.Instance.CloseUI("MemuPanel");
            UISystem.Instance.CloseUI("DownTime");
            EnemyManager.Instance.QuitGameScene();
            MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
        }


        /// <summary>
        /// 伤害运算
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="target">目标受伤者</param>
        /// <param name="item">释放的技能</param>
        /// <param name="BoundPoint">命中点</param>
        public void OptionDamage(IDamage attack,IDamage target,SkillItem item,Vector3 BoundPoint)
        {
            //1.伤害技能计算算法  ： 角色（基础力量 * 造成的伤害）*技能攻击力
            CharacterState attackState = attack.GetState();
            CharacterState targetState = target.GetState();
            //1.1 获取攻击者的基础力量*物理攻击力
            switch (item.SkillType.type)
            {
                case DamageType.Physics:
                    var Physics = attackState.PhysicsAttack * (1 + 0.004 * attackState.Power) * (1 + (attackState.SkillAttack / 10));
                    //1.基础攻击力 = (物理攻击力 * （1+0.004*力量）*技能攻击力*暴击伤害
                    bool isCirtical = attackState.Cirtical > Random.value;
                    if (isCirtical)
                    {
                        //暴击了
                        // ReSharper disable once PossibleLossOfFraction
                        Physics *= (2.5d+attackState.CirticalAttack/10);
                    }

                    Physics += item.Diamage;
                    //2.基础攻击力加技能基础伤害
                    target.IDamage((int)Math.Round(Physics,0));
                    DamageTextItem damageTextItem  = SkillPoolManager.Release(DamageWordUI,BoundPoint,Quaternion.identity).GetComponent<DamageTextItem>();
                    damageTextItem.Show(DamageType.Physics,isCirtical,((int)Math.Round(Physics,0)).ToString());
                    break;
                case DamageType.Magic:
                    var Magic = attackState.MagicAttack * (1 + 0.004 * attackState.Intelligence)*(1+(attackState.SkillAttack/100) + (1+attackState.CirticalAttack/100));
                    //1.基础攻击力 = (魔法攻击力 * （1+0.004*智力）*技能攻击力*暴击伤害
                    Magic += item.Diamage;
                    //2.基础攻击力加技能基础伤害
                    target.IDamage((int)Math.Round(Magic,0));
                    break;
                case DamageType.Treatment:
                    break;
                default:
                    Debug.Log("未知的伤害类型，请检查");
                    break;
            }
        }

    }

}

