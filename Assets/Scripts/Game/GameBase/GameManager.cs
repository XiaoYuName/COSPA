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
    public class GameManager : MonoSingleton<GameManager>
    { 
        //角色的公用预制体
        [HideInInspector]public Character Player;
        /// <summary>
        /// 当前战斗的副本
        /// </summary>
        private RegionItem currentRegion;
        
        private CinemachineVirtualCamera virtualCamera;
        private GameObject DamageWordUI;
        private Vector2Int currentPress;
        
        protected override void Awake()
        {
            base.Awake();
            
            DamageWordUI = GameSystem.Instance.GetPrefab("DamageText");
        }
        
        
        /// <summary>
        /// 初始化加载战斗场景
        /// </summary>
        /// <param name="bags">玩家列表</param>
        /// <param name="pos">玩家位置</param>
        /// <param name="regionItem">敌人配置</param>
        public IEnumerator StarSceneGame(CharacterBag bags,Vector3 pos,RegionItem regionItem)
        {
            currentRegion = regionItem;
            currentPress = regionItem.Press;
            var data = InventoryManager.Instance.GetCharacter(bags.ID);
            var Obj = data.Prefab.GetComponent<Character>();
            Player =  Instantiate(Obj, pos, Quaternion.identity);
            Player.Init(bags);
            Player.isAI = false;
            UISystem.Instance.OpenUI("GameMemu");
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            virtualCamera.Follow = Player.transform;
            SceenDestruction.Instance._Camera = Camera.main;
            ARPG.Pool.Skill.SkillPoolManager.Instance.AddPoolPrefab(new Pool.Skill.Pool
            {
                prefab = DamageWordUI,
                count =  100,
            });
            yield return EnemyManager.Instance.CreateEnemy(regionItem);
            ARPG.Pool.Skill.SkillPoolManager.Instance.Init();
        }

        /// <summary>
        /// 强制退出战斗场景函数
        /// </summary>
        public void QuitGameScene()
        {
            UISystem.Instance.CloseUI("GameMemu");
            UISystem.Instance.CloseUI("MemuPanel");
            UISystem.Instance.CloseUI("DownTime");
            UISystem.Instance.CloseUI("PlayerState");
            UISystem.Instance.CloseUI("BossStateUI");
            DynamicJoystick joystick = UISystem.Instance.GetNotBaseUI<DynamicJoystick>("DynamicJoystick");
            joystick.gameObject.SetActive(false);
            UISystem.Instance.CloseUI("AttackButton");
            BUFFManager.Instance.RemoveDictionary(Player);
            EnemyManager.Instance.QuitGameScene();
            Destroy(Player.gameObject);
            MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
        }

        private Coroutine _coroutine;
        /// <summary>
        /// 战斗胜利，结算完毕后退出战斗场景
        /// </summary>
        public void VictoryGameScene()
        {
            UISystem.Instance.CloseUI("GameMemu");
            UISystem.Instance.CloseUI("DownTime");
            UISystem.Instance.CloseUI("PlayerState");
            DynamicJoystick joystick = UISystem.Instance.GetNotBaseUI<DynamicJoystick>("DynamicJoystick");
            joystick.gameObject.SetActive(false);
            TaskManager.Instance.TriggerTask(TaskTrigger.通关地下城,1);
            UISystem.Instance.CloseUI("AttackButton");
            StartCoroutine(WaitPlayAnimator());
        }
        
        /// <summary>
        /// 战斗结束时，如果Player还在表演动画，则等待动画表演完毕，再进行进入到结算动画
        /// </summary>
        /// <returns></returns>
        private IEnumerator WaitPlayAnimator()
        {
            Vector3 wordPoint = Camera.main.ViewportToWorldPoint(Settings.zeroView);
            wordPoint.z = 0;
            while (Player.animSpeed <= 0)  //如果Player正在播放其他动画，则等待动画播放完毕
            {
                yield return null;
            }
            Player.animSpeed = 0;
            Player.isAI = true;
            Player.rb.velocity = Vector2.zero;
            _coroutine ??= StartCoroutine(MovZeroPoint(wordPoint));
        }

        /// <summary>
        /// 移動玩家到UI中心點位置
        /// </summary>
        /// <param name="zeroPoint"></param>
        /// <returns></returns>
        private IEnumerator MovZeroPoint(Vector3 zeroPoint)
        {
            Player.transform.rotation = Quaternion.Euler(0,0,0);
            while (Vector3.Distance(Player.transform.localPosition,zeroPoint) >0.5f)
            {
                Player.transform.localPosition = Vector3.MoveTowards(Player.transform.localPosition, zeroPoint, 3.5f*Time.deltaTime);
                Player.anim.SetBool("isMovenemt",true);
                yield return null;
            }
            //2.播放胜利庆祝动画
            Player.anim.SetBool("isMovenemt",false);
            Player.anim.SetTrigger("Victory");
            //3.显示胜利UI
            yield return new WaitForSeconds(1.25f);
            void Func(GameEnd ui)
            {
                if (Settings.isRandomRegion(currentRegion.RegionItemName))
                {
                    ui.ShowEndGame(GameSystem.Instance.GetRandomMap(currentRegion.RegionItemName));
                }
                else
                {
                    ui.ShowEndGame(GameSystem.Instance.GetMapReword(currentRegion.RegionItemName));
                }
            }
            AudioManager.Instance.PlayAudio("VictoryGame");
            UISystem.Instance.OpenUI<GameEnd>("GameEnd",Func);
            _coroutine = null;
        }

        /// <summary>
        /// 结算完结，退出战斗场景,回到GameScene场景
        /// </summary>
        public void VictoryQuitScene()
        {
            UISystem.Instance.CloseUI("GameEnd");
            UISystem.Instance.CloseUI("GameMemu");
            UISystem.Instance.CloseUI("MemuPanel");
            UISystem.Instance.CloseUI("DownTime");
            EnemyManager.Instance.QuitGameScene();
            BUFFManager.Instance.RemoveDictionary(Player);
            Destroy(Player.gameObject);
            InventoryManager.Instance.SetPress(currentPress);
            MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
        }

        /// <summary>
        /// 玩家死亡，弹出死亡界面UI,之后等待点击按钮后回到主界面GameScnen场景
        /// </summary>
        public void GameOverScnen()
        {
            StartCoroutine(GameOverWait());
        }
        

        /// <summary>
        /// 死亡结算界面，写成携程是希望之后添加其他DoTween动画
        /// </summary>
        /// <returns></returns>
        private IEnumerator GameOverWait()
        {
            UISystem.Instance.CloseUI("GameMemu");
            UISystem.Instance.CloseUI("DownTime");
            UISystem.Instance.CloseUI("PlayerState");
            UISystem.Instance.CloseUI("BossStateUI");
            DynamicJoystick joystick = UISystem.Instance.GetNotBaseUI<DynamicJoystick>("DynamicJoystick");
            joystick.gameObject.SetActive(false);
            UISystem.Instance.CloseUI("AttackButton");
            EnemyManager.Instance.QuitGameScene();
            yield return new WaitForSeconds(1);
            void Func(GameEnd ui)
            {
                ui.ShowGameOver();
            }
            UISystem.Instance.OpenUI<GameEnd>("GameEnd",Func);
            
        }

        /// <summary>
        /// 伤害运算
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="target">目标受伤者</param>
        /// <param name="item">释放的技能</param>
        /// <param name="BoundPoint">命中点</param>
        /// <param name="isMultisTag">是否启用延迟多段上海,默认启用</param>
        public void OptionDamage(IDamage attack,IDamage target,SkillItem item,Vector3 BoundPoint,bool isMultisTag = true)
        {
            if (target == null && item.SkillType.type != DamageType.Treatment) return;
            
            if(item.SkillType.type != DamageType.Treatment)
                if (target.GetState().currentHp <= 0) return; //防止多段伤害一直显示掉血
            //1.伤害技能计算算法  ： 角色（基础力量 * 造成的伤害）*技能攻击力
            float NextBuffVlaue = BUFFManager.Instance.GetNextDicTypeValue(attack.GetBuffLogic(), BuffTrigger.累计攻击, StateMode.最终伤害);
            if (item.SkillType.type == DamageType.Treatment)
            {
                OptionAddHp(attack,item,BoundPoint);
                if (item.SkillType.isMultistage)
                {
                    StartCoroutine(WaitMultistage(attack,item,BoundPoint));
                }
                return;
            }
            //多段运算
            if (item.SkillType.isMultistage && isMultisTag)
            {
                StartCoroutine(WaitMultistageAttack(attack,target,item,BoundPoint));
                return;
            }

            CharacterState attackState = attack.GetState();
            CharacterState targetState = target.GetState();
            float BuffValue = BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.伤害,StateMode.最终伤害);//最终伤害值
            //1.1 获取攻击者的基础力量*物理攻击力
            switch (item.SkillType.type)
            {
                case DamageType.Physics:
                    var Physics = attackState.PhysicsAttack * (1 + 0.004 * attackState.Power) * (1 + (attackState.SkillAttack / 100));
                    //1.基础攻击力 = (物理攻击力 * （1+0.004*力量）*技能攻击力*暴击伤害
                    bool isCirtical = attackState.Cirtical > Random.value;
                    if (isCirtical)
                    {
                        //暴击了
                        // ReSharper disable once PossibleLossOfFraction
                        Physics *= (attackState.CirticalAttack +attackState.CirticalAttack/100);
                    }
                    //1.1 计算基础攻击力
                    Physics += item.Diamage;
                    //2.  扣除防御力加成
                    Physics -= (targetState.Defense+BUFFManager.Instance.GetTyepValue(target.GetBuffLogic(),BuffType.增益,StateMode.防御力));
                    //3.计算BUFF加成
                    Physics += BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.增益, StateMode.物理攻击力);
                    //4.计算最终伤害
                    Physics *= (1+(BuffValue/100));
                    Physics *= (1 + (NextBuffVlaue / 100));
                    Physics = Mathf.Max(1, (int)Physics);
                    target.IDamage((int)Math.Round(Physics,0));
                    DamageTextItem damageTextItem  = SkillPoolManager.Release(DamageWordUI,BoundPoint,Quaternion.identity).GetComponent<DamageTextItem>();
                    damageTextItem.Show(DamageType.Physics,isCirtical,((int)Math.Round(Physics,0)).ToString());
                    return;
                case DamageType.Magic:
                    var Magic = attackState.MagicAttack * (1 + 0.004 * attackState.Intelligence)*(1+(attackState.SkillAttack/100));
                    //1.基础攻击力 = (魔法攻击力 * （1+0.004*智力）*技能攻击力*暴击伤害
                    bool isMagicCirtical = attackState.Cirtical > Random.value;
                    if (isMagicCirtical)
                    {
                        //暴击了
                        // ReSharper disable once PossibleLossOfFraction
                        Magic *= (attackState.CirticalAttack +attackState.CirticalAttack/100);
                    }
                    Magic += item.Diamage;
                    //1.1 伤害要减去地方防御力
                    Magic -= (targetState.Defense+BUFFManager.Instance.GetTyepValue(target.GetBuffLogic(),BuffType.增益,StateMode.防御力));
                    //2.基础攻击力加技能基础伤害
                    Magic += BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.增益, StateMode.魔法攻击力);
                    Magic *= (1+(BuffValue/100));
                    Magic *= (1 + (NextBuffVlaue / 100));
                    Magic = Mathf.Max(1, (int)Magic);
                    target.IDamage((int)Math.Round(Magic,0));
                    DamageTextItem damageText = SkillPoolManager.Release(DamageWordUI,BoundPoint,Quaternion.identity).GetComponent<DamageTextItem>();
                    damageText.Show(DamageType.Magic,isMagicCirtical,((int)Math.Round(Magic,0)).ToString());
                    return;
                default:
                    Debug.Log("未知的伤害类型，请检查");
                    return;
            }
        }

        /// <summary>
        /// 回复技能运算
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="item">技能数据</param>
        /// <param name="Point">命中点坐标</param>
        private void OptionAddHp(IDamage attack, SkillItem item,Vector3 Point)
        {
            CharacterState attackState = attack.GetState();
            double addHp = attackState.AddHp * (1 + (attackState.SkillAttack / 100));
            float BuffValue = BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.伤害,StateMode.治疗量);//最终伤害值
            addHp += item.Diamage;
            //2.基础攻击力加技能基础伤害
            addHp *= (1+(BuffValue/100));
            addHp = Mathf.Max(1, (int)addHp);
            attack.IReply((int)Math.Round(addHp,0));
            DamageTextItem damageTextItem  = SkillPoolManager.Release(DamageWordUI,Point,Quaternion.identity).GetComponent<DamageTextItem>();
            damageTextItem.Show(DamageType.Treatment,false,((int)Math.Round(addHp,0)).ToString());
        }

        /// <summary>
        /// 回复技能运算
        /// </summary>
        /// <param name="attack">技能发动者</param>
        /// <param name="value">基础数值</param>
        private void OptionAddHp(IDamage attack, int value)
        {
            CharacterState attackState = attack.GetState();
            double addHp = attackState.AddHp * (1 + (attackState.SkillAttack / 100));
            float BuffValue = BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.伤害,StateMode.治疗量);//最终伤害值
            addHp += value;
            //2.基础攻击力加技能基础伤害
            addHp *= (1+(BuffValue/100));
            addHp = Mathf.Max(1, (int)addHp);
            attack.IReply((int)Math.Round(addHp,0));
            DamageTextItem damageTextItem  = SkillPoolManager.Release(DamageWordUI,attack.GetPoint(),Quaternion.identity).GetComponent<DamageTextItem>();
            damageTextItem.Show(DamageType.Treatment,false,((int)Math.Round(addHp,0)).ToString());
        }
        
        /// <summary>
        /// 延迟多段回复效果
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="item">技能配置</param>
        /// <param name="Point">发动点</param>
        /// <returns></returns>
        private IEnumerator WaitMultistage(IDamage attack, SkillItem item,Vector3 Point)
        {
            yield return new WaitForSeconds(item.SkillType.MultistageTime);
            if (Player != null)
            {
                for (int i = 0; i < item.SkillType.MultistageDamage.Count; i++)
                {
                    OptionAddHp(attack,item.SkillType.MultistageDamage[i]);
                    yield return new WaitForSeconds(item.SkillType.MultistageTime);
                }
            }
        }

        /// <summary>
        /// 延迟
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="target">目标敌方</param>
        /// <param name="item">技能数据信息</param>
        /// <param name="Point">命中点位置</param>
        /// <returns></returns>
        private IEnumerator WaitMultistageAttack(IDamage attack,IDamage target,SkillItem item,Vector3 Point)
        {
            if (Player != null)
            {
                float NextBuffVlaue = BUFFManager.Instance.GetNextDicTypeValue(attack.GetBuffLogic(), BuffTrigger.累计攻击, StateMode.最终伤害);
                for (int i = 0; i < item.SkillType.MultistageDamage.Count; i++)
                {
                    CharacterState attackState = attack.GetState();
                    CharacterState targetState = target.GetState();
                    if(targetState.currentHp<=0) continue;
                    float BuffValue = BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.伤害, StateMode.最终伤害); //最终伤害值
                    //1.1 获取攻击者的基础力量*物理攻击力
                    switch (item.SkillType.type)
                    {
                        case DamageType.Physics:
                            var Physics = attackState.PhysicsAttack * (1 + 0.004 * attackState.Power) *
                                          (1 + (attackState.SkillAttack / 100));
                            //1.基础攻击力 = (物理攻击力 * （1+0.004*力量）*技能攻击力*暴击伤害
                            bool isCirtical = attackState.Cirtical > Random.value;
                            if (isCirtical)
                            {
                                //暴击了
                                // ReSharper disable once PossibleLossOfFraction
                                Physics *= (attackState.CirticalAttack  + attackState.CirticalAttack / 100);
                            }
                            //1.1 计算基础攻击力
                            Physics += item.SkillType.MultistageDamage[i];
                            //2.  扣除防御力加成
                            Physics -= (targetState.Defense + BUFFManager.Instance.GetTyepValue(target.GetBuffLogic(), BuffType.增益, StateMode.防御力));
                            //3.计算BUFF加成
                            Physics += BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.增益,
                                StateMode.物理攻击力);
                            //4.计算最终伤害
                            Physics *= (1 + (BuffValue / 100));
                            Physics *= (1 + (NextBuffVlaue / 100));
                            Physics = Mathf.Max(1, (int)Physics);
                            target.IDamage((int)Math.Round(Physics, 0));
                            DamageTextItem damageTextItem = SkillPoolManager.Release(DamageWordUI, Point, Quaternion.identity).GetComponent<DamageTextItem>();
                            damageTextItem.Show(DamageType.Physics, isCirtical, ((int)Math.Round(Physics, 0)).ToString());
                            break;
                        case DamageType.Magic:
                            var Magic = attackState.MagicAttack * (1 + 0.004 * attackState.Intelligence) *
                                        (1 + (attackState.SkillAttack / 100) + (1 + attackState.CirticalAttack / 100));
                            //1.基础攻击力 = (魔法攻击力 * （1+0.004*智力）*技能攻击力*暴击伤害
                            bool isMagicCirtical = attackState.Cirtical > Random.value;
                            if (isMagicCirtical)
                            {
                                //暴击了
                                // ReSharper disable once PossibleLossOfFraction
                                Magic *= (attackState.CirticalAttack + attackState.CirticalAttack / 100);
                            }
                            Magic += item.SkillType.MultistageDamage[i];
                            //1.1 伤害要减去地方防御力
                            Magic -= (targetState.Defense + BUFFManager.Instance.GetTyepValue(target.GetBuffLogic(), BuffType.增益, StateMode.防御力));
                            //2.基础攻击力加技能基础伤害
                            Magic += BUFFManager.Instance.GetTyepValue(attack.GetBuffLogic(), BuffType.增益, StateMode.魔法攻击力);
                            Magic *= (1 + (BuffValue / 100));
                            Magic *= (1 + (NextBuffVlaue / 100));
                            Magic = Mathf.Max(1, (int)Magic);
                            target.IDamage((int)Math.Round(Magic, 0));
                            DamageTextItem damageText = SkillPoolManager.Release(DamageWordUI, Point, Quaternion.identity).GetComponent<DamageTextItem>();
                            damageText.Show(DamageType.Magic, isMagicCirtical, ((int)Math.Round(Magic, 0)).ToString());
                            break;
                        default:
                            Debug.Log("未知的伤害类型，请检查");
                            break;
                    }
                    yield return new WaitForSeconds(item.SkillType.MultistageTime);
                }
            }
        }

        /// <summary>
        /// 多个敌人命中伤害类型类型运算
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="targets">目标列表</param>
        /// <param name="item">使用的技能</param>
        /// <param name="BoundPoint">命中点</param>
        /// <param name="isMultisTag">是否启用延迟多段伤害,默认启用</param>
        public void OptionAllDamage(IDamage attack, IDamage[] targets, SkillItem item, Vector3[] BoundPoint,bool isMultisTag = true)
        {
            if (BoundPoint.Length != targets.Length)
                throw new Exception("命中点与敌人数不匹配");

            for (int i = 0; i < targets.Length; i++)
            {
                OptionDamage(attack,targets[i],item,BoundPoint[i],isMultisTag);
            }
        }

        /// <summary>
        /// 多个敌人命中伤害类型类型运算
        /// </summary>
        /// <param name="attack">攻击者</param>
        /// <param name="targets">目标列表</param>
        /// <param name="item">使用的技能</param>
        /// <param name="isMultisTag">是否启用延迟多段伤害,默认启用</param>
        public void OptionAllDamage(IDamage attack, IDamage[] targets, SkillItem item,bool isMultisTag = true)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                OptionDamage(attack,targets[i],item,targets[i].GetPoint(),isMultisTag);
            }
        }
        
       


        /// <summary>
        /// 解除当前场景的视角跟随
        /// </summary>
        public void RelieveFollowPlayer()
        {
            virtualCamera.Follow = null;
        }

    }

}

