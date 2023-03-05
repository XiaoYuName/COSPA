using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARPG.Config;
using ARPG.UI;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace ARPG
{
    public  class Character : MonoBehaviour,IDamage,IBuffLogic
    {
        #region PropComponent
        /// <summary>
        /// 在背包的数据
        /// </summary>
        [HideInInspector] public CharacterBag currentBag;
        [SerializeField]protected CharacterState State;
        protected CharacterConfigInfo data;
        protected SkeletonMecanim Spine;
        [HideInInspector]public Animator anim;
        [HideInInspector]public AttackButton attackButton;
        private PlayerState StateUI;
        [HideInInspector]public float animSpeed = 1; //动画驱动的移动速度，该速度控制在动画播放过程中,能否能进行重复操作，或者切换动画
        [HideInInspector]public bool isAI;
        public Collider2D DamageCollider2D;
        public Collider2D GroundCollider;
        private SkeletonUtilityBone[] bones;


        //--------------------------Movenemt--------------------------//
        [HideInInspector]public Rigidbody2D rb;
        /// <summary>
        /// 摇杆组件
        /// </summary>
        private DynamicJoystick Joystick;
        private Vector2 InputSpeed;
        private static readonly int s_IsMovenemt = Animator.StringToHash("isMovenemt");
        private static readonly int s_Attack = Animator.StringToHash("Attack");
        //-------------------------Skill-------------------------------//
        /// <summary>
        /// 技能对象子弹,初始化阶段会加载出所有的技能对象,并执行Init初始化,之后在释放时调用Play方法
        /// </summary>
        public Dictionary<SkillType, Skill> SkillDic = new Dictionary<SkillType, Skill>();

        [HideInInspector]public Transform body;
        private static readonly int s_Die = Animator.StringToHash("Die");
        private static readonly int s_Damage = Animator.StringToHash("Damage");
        //---------------------------Buff--------------------------------//
        [HideInInspector]public List<IBuff> Buffs = new List<IBuff>();
        private Dictionary<BuffTrigger, Dictionary<IBuff, int>> BuffNext;
        public Dictionary<StopTrigger, Dictionary<IBuff,Action>> stopAttackEvent = new Dictionary<StopTrigger, Dictionary<IBuff, Action>>();

        private Dictionary<EndTrigger, Dictionary<IBuff, Action>> EndBuffTriggers =
            new Dictionary<EndTrigger, Dictionary<IBuff, Action>>();
        #endregion

        #region Character
        private void Awake()
        {
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
            anim = Spine.GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            Joystick = UISystem.Instance.GetNotBaseUI<DynamicJoystick>("DynamicJoystick");
            Joystick.gameObject.SetActive(true);
            attackButton = UISystem.Instance.GetUI<AttackButton>("AttackButton");
            DamageCollider2D = transform.Find("DamageCollier").GetComponent<Collider2D>();
            GroundCollider = GetComponent<Collider2D>();
            StateUI = UISystem.Instance.GetUI<PlayerState>("PlayerState");
        }

        public void Init(CharacterBag bag)
        {
            isAI = false;
            currentBag = bag;
            data = InventoryManager.Instance.GetCharacter(bag.ID);
            State = bag.CurrentCharacterState.Clone() as CharacterState;
            //生成的时候赋值一次当前生命值=最大生命值
            State.currentHp = State.HP;
            StateUI.InitData(data,bag,State);
            Spine.skeletonDataAsset = data.GetAssets(bag.currentStar).Spinedata;
            Spine.Initialize(true);
            attackButton.InitBindButton(Attack,Skill_1,Skill_2,Skill_3,Skill_4);
            anim.runtimeAnimatorController = data.AnimatorController;
            body = transform.Find("Spine/SkeletonUtility-SkeletonRoot/root");
            BuffNext = new Dictionary<BuffTrigger, Dictionary<IBuff, int>>();
            stopAttackEvent = new Dictionary<StopTrigger, Dictionary<IBuff, Action>>();
            bones  = transform.GetComponentsInChildren<SkeletonUtilityBone>();
            CreateSkillClass();
            CreatBuff();
        }

        private void CreateSkillClass()
        {
            SkillDic.Clear();
            stopAttackEvent.Clear();
            for (int i = 0; i < data.SkillTable.Length; i++)
            {
                if(String.IsNullOrEmpty(data.SkillTable[i].SkillID))continue;
                SkillItem skillItem = GameSystem.Instance.GetSkill(data.SkillTable[i].SkillID);
                if (currentBag.currentStar < skillItem.ActionStar)
                {
                    attackButton.SetUI(data.SkillTable[i].Type, null);
                    return;
                }
                
                Type type = Type.GetType("ARPG." +skillItem.ID);
                if (type == null) return;
                Skill skill = Activator.CreateInstance(type) as Skill;
                SkillDic.Add(data.SkillTable[i].Type,skill);
                if (skill != null) skill.Init(this,data.SkillTable[i].Type,skillItem);
                attackButton.SetUI(data.SkillTable[i].Type, skillItem);
                foreach (var pool in skillItem.Pools)
                {
                    ARPG.Pool.Skill.SkillPoolManager.Instance.AddPoolPrefab(pool);
                }
            }
        }

        private void Update()
        {
            InputSpeed.x = Joystick.Horizontal;
            InputSpeed.y = Joystick.Vertical;
            SwitchAnimation();
        }

        private void FixedUpdate()
        {
            if(!isAI)
                Movenemt();
        }

        private void OnDestroy()
        {
            foreach (var skill in SkillDic)
            {
                SkillDic[skill.Key].UHandle();
            }
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movenemt()
        {
            if (InputSpeed != Vector2.zero)
            {
                //加成：面板移动速度+BUFF增加移动速度+动画播放速度
                rb.velocity = InputSpeed.normalized * State.MovSpeed * animSpeed * BUFFManager.Instance.GetTyepValue(this, BuffType.增益, StateMode.移动速度) * Time.fixedDeltaTime;
                BuffTriggerEvent(BuffTrigger.移动时);
                BuffAddTrigger(BuffTrigger.累计移动);
                TriggerEndEvent(EndTrigger.移动时);
            }
            else
            {
                rb.velocity = Vector2.zero;
                BuffTriggerEvent(BuffTrigger.站立时);
            }
            Flip();
        }

        /// <summary>
        /// 转向
        /// </summary>
        private void Flip()
        {
            if (InputSpeed != Vector2.zero && animSpeed != 0)
            {
                transform.rotation = Quaternion.Euler(0,InputSpeed.x <0 ? 180:0,0);
            }
        }

        /// <summary>
        /// 设置动画
        /// </summary>
        private void SwitchAnimation()
        {
            if(!isAI)
                anim.SetBool(s_IsMovenemt,!(InputSpeed == Vector2.zero));
        }
        #endregion
        
        #region Skill
        /// <summary>
        /// 普攻技能回调函数
        /// </summary>
        protected  void Attack()
        {
            if(animSpeed == 0)return;
            anim.SetTrigger(s_Attack);
            SkillDic[SkillType.Attack].Play();
            BuffTriggerEvent(BuffTrigger.攻击时);
            BuffAddTrigger(BuffTrigger.累计攻击);
            TriggerStopEvent(StopTrigger.攻击时);
            TriggerEndEvent(EndTrigger.攻击时);
            
            var audioID = data.GetSkillNameID(SkillType.Attack).BtnAudioID;
            if (audioID != null && !String.IsNullOrEmpty(audioID))
            {
                AudioManager.Instance.PlayAudio(audioID);
            }
        }


        /// <summary>
        /// 技能1回调函数
        /// </summary>
        protected  void Skill_1()
        {
            if(SkillDic.ContainsKey(SkillType.Skill_01))
                SkillDic[SkillType.Skill_01].Play();
            
            var audioID = data.GetSkillNameID(SkillType.Skill_01).BtnAudioID;
            if (audioID != null && !String.IsNullOrEmpty(audioID))
            {
               AudioManager.Instance.PlayAudio(audioID);
            }
        }
    
        /// <summary>
        /// 技能2回调函数
        /// </summary>
        protected  void Skill_2()
        {
            if(SkillDic.ContainsKey(SkillType.Skill_02))
                SkillDic[SkillType.Skill_02].Play();
            
            var audioID = data.GetSkillNameID(SkillType.Skill_02).BtnAudioID;
            if (audioID != null && !String.IsNullOrEmpty(audioID))
            {
                AudioManager.Instance.PlayAudio(audioID);
            }
        }

        /// <summary>
        /// 技能3回调函数
        /// </summary>
        protected  void Skill_3()
        {
            if(SkillDic.ContainsKey(SkillType.Skill_03))
                SkillDic[SkillType.Skill_03].Play();
            
            var audioID = data.GetSkillNameID(SkillType.Skill_03).BtnAudioID;
            if (audioID != null && !String.IsNullOrEmpty(audioID))
            {
                AudioManager.Instance.PlayAudio(audioID);
            }
        }

        /// <summary>
        /// 觉醒回调函数
        /// </summary>
        protected void Skill_4()
        {
            if(SkillDic.ContainsKey(SkillType.Evolution))
                SkillDic[SkillType.Evolution].Play();
            
            var audioID = data.GetSkillNameID(SkillType.Evolution).BtnAudioID;
            if (audioID != null && !String.IsNullOrEmpty(audioID))
            {
                AudioManager.Instance.PlayAudio(audioID);
            }
        }
        #endregion
        
        #region IDamage
        
        public CharacterState GetState()
        {
            return State;
        }

        public Vector3 GetPoint()
        {
            try
            {
                return gameObject == null ? Vector3.zero : body.transform.position;
            }
            catch (Exception)
            {
                return Vector3.zero;
            }
        }

        /// <summary>
        /// 获取骨骼位置,该位置跟随Spine动画位置
        /// </summary>
        /// <param name="BodyName">骨骼</param>
        /// <returns>返回对应骨骼根节点位置，如果找不到,则直接返回Character原点</returns>
        public Transform GetPoint(string BodyName)
        {
            if (bones == null) return transform;
            foreach (var bone in bones)
            {
                if (bone.bone.ToString() == BodyName)
                {
#if UNITY_EDITOR
                    Debug.Log("找到对应:"+BodyName+"骨骼跟随节点");
#endif
                    return bone.transform;
                }
            }
            return transform;
        }


        public void IDamage(int Damage)
        {
            State.currentHp -= Damage;
            if (State.currentHp <= 0)
            {
                State.currentHp = 0;
                anim.SetTrigger(s_Die);
                isAI = true;
                rb.velocity = Vector2.zero;
                GetStateUI().RemoveAll_UI();
                GameManager.Instance.GameOverScnen();
                return;
            }
            StateUI.UpdateState(State);
            anim.SetTrigger(s_Damage);
            BuffTriggerEvent(BuffTrigger.受击时);
        }

        public void IReply(int Reply)
        {
            State.currentHp = Mathf.Min(State.currentHp+Reply,State.HP);
            StateUI.UpdateState(State);
            BuffTriggerEvent(BuffTrigger.回复自身时);
        }
        #endregion
        
        #region IBUFF
        /// <summary>
        /// 触发触发器
        /// </summary>
        /// <param name="type">触发类型</param>
        public void BuffTriggerEvent(BuffTrigger type)
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                Buffs[i].Trigger(type);
            }
        }

        /// <summary>
        /// 触发连击类BUFF
        /// </summary>
        /// <param name="type">触发器类型</param>
        private void BuffAddTrigger(BuffTrigger type)
        {
            if (BuffNext.ContainsKey(type))
            {
                for (int i = 0; i < BuffNext[type].Count; i++)
                {
                    (IBuff Key, int value) = BuffNext[type].ElementAt(i);
                    if (BuffNext[type].ContainsKey(Key))
                    {
                        int newValue = value+1;
                        RefDicBUFF(type,Key,newValue);
                    }
                }
            }
        }

        /// <summary>
        /// 添加Stop 触发器类型的BUFF 触发
        /// </summary>
        /// <param name="trigger">触发器类型</param>
        /// <param name="buff">BUFF</param>
        /// <param name="action">回调函数</param>
        private void AddStopEvent(StopTrigger trigger,IBuff buff, Action action)
        {
            if (!stopAttackEvent.ContainsKey(trigger))
            {
                stopAttackEvent.Add(trigger,new Dictionary<IBuff, Action>());
            }

            if (!stopAttackEvent[trigger].ContainsKey(buff))
            {
                stopAttackEvent[trigger].Add(buff,action);
            }
            stopAttackEvent[trigger][buff] = action;
        }

        /// <summary>
        /// 添加End 触发器类型的BUFF 触发
        /// </summary>
        /// <param name="trigger">触发器类型</param>
        /// <param name="buff">BUFF</param>
        /// <param name="action">回调函数</param>
        public void AddEndBuff(EndTrigger trigger, IBuff buff, Action action)
        {
            if (!EndBuffTriggers.ContainsKey(trigger))
            {
                EndBuffTriggers.Add(trigger,new Dictionary<IBuff, Action>());
            }

            if (!EndBuffTriggers[trigger].ContainsKey(buff))
            {
                EndBuffTriggers[trigger].Add(buff,action);
            }
            EndBuffTriggers[trigger][buff] = action;
        }

        /// <summary>
        /// 触发所有Stop 类型的trigger
        /// </summary>
        /// <param name="trigger">触发器类型</param>
        public void TriggerStopEvent(StopTrigger trigger)
        {
            if (stopAttackEvent.ContainsKey(trigger))
            {
                for (int i = 0; i < stopAttackEvent[trigger].Count; i++)
                {
                    (IBuff Item, Action action) = stopAttackEvent[trigger].ElementAt(i);
                    if (Item.data.StopTrigger == trigger)
                    {
                        action?.Invoke();
                        stopAttackEvent[trigger].Remove(Item);
                    }
                }
            }
        }

        /// <summary>
        /// 触发End 触发器内所有Trigger 类型的BUFF
        /// </summary>
        /// <param name="trigger">触发器类型</param>
        public void TriggerEndEvent(EndTrigger trigger)
        {
            if (EndBuffTriggers.ContainsKey(trigger))
            {
                for (int i = 0; i < EndBuffTriggers[trigger].Count; i++)
                {
                    (IBuff Item, Action action) = EndBuffTriggers[trigger].ElementAt(i);
                    if (Item.data.Trigger == trigger)
                    {
                        action?.Invoke();
                        EndBuffTriggers[trigger].Remove(Item);
                    }
                }
            }
        }

        /// <summary>
        /// 实例化BUFF并添加到自身
        /// </summary>
        private void CreatBuff()
        {
            for (int i = 0; i < data.deftualBuffID.Count; i++)
            {
                BuffData newBuff = ConfigSystem.Instance.GetBUFFData(data.deftualBuffID[i].ToString());
                if (newBuff.behaviourType == BuffBehaviourType.光环) 
                    GetStateUI().AddBuffItemUI(newBuff.ToBuff(this));
                IBuff newIBuff = newBuff.ToBuff(this);
                Buffs.Add(newIBuff);
                RefDicBUFF(newBuff.buffTrigger, newIBuff, 0);
            }
        }

        /// <summary>
        /// 获取自身IBuffLogic 接口
        /// </summary>
        /// <returns></returns>
        public IBuffLogic GetBuffLogic()
        {
            return this;
        }

        /// <summary>
        /// 获取显示器UI组件
        /// </summary>
        /// <returns></returns>
        public BuffStateUI GetStateUI()
        {
            return StateUI.GetBuffStateUI();
        }

        /// <summary>
        /// 获取IDamage 接口
        /// </summary>
        /// <returns></returns>
        public IDamage GetIDamage()
        {
            return this;
        }
        
        /// <summary>
        /// 添加BUFF到
        /// </summary>
        /// <param name="buff"></param>
        public void AddBuff(BuffData buff)
        {
            BuffData newBuff = buff;
            IBuff newIBuff = newBuff.ToBuff(this);
            if (newBuff.behaviourType == BuffBehaviourType.光环) 
                GetStateUI().AddBuffItemUI(newIBuff);
            Buffs.Add(newIBuff);
            RefDicBUFF(newBuff.buffTrigger, newIBuff, 0);
            if (BUFFManager.Instance.isNextType(buff.buffTrigger))
            {
                GetStateUI().AddBuffItemUI(newIBuff,0);
            }
        }

        /// <summary>
        /// 添加End Buff到触发器字典
        /// </summary>
        /// <param name="trigger">触发类型</param>
        /// <param name="IBuff">BUFF 实例</param>
        /// <param name="action">回调函数</param>
        public void AddBuffEvent(EndTrigger trigger, IBuff IBuff, Action action)
        {
            AddEndBuff(trigger,IBuff,action);
        }


        /// <summary>
        /// 更新BUFF 连击表
        /// </summary>
        /// <param name="trigger">触发类型</param>
        /// <param name="IBuff">BUFF实例</param>
        /// <param name="value">值</param>
        private void RefDicBUFF(BuffTrigger trigger,IBuff IBuff,int value)
        {
            if (!BuffNext.ContainsKey(trigger))
            {
                BuffNext.Add(trigger,new Dictionary<IBuff, int>());
            }
            if (!BuffNext[trigger].ContainsKey(IBuff))
            {
                BuffNext[trigger].Add(IBuff,0);
            }
            if (value >= 0)
            {
                BuffNext[trigger][IBuff] = Mathf.Min(value,IBuff.data.maxLevel);
                if (BUFFManager.Instance.isNextType(IBuff.data.buffTrigger))
                    GetStateUI().RefBUFF_UI(IBuff, value);
            }
            if (IBuff.data.NextLevel >=0 && value >= IBuff.data.NextLevel) //满足条件,触发器触发后归0
            {
                void Action()
                {
                    GetStateUI().RemoveBUFF_UI(IBuff);
                    IBuff.Trigger(trigger);
                    BuffNext[trigger][IBuff] = 0;
                }

                if (IBuff.data.Trigger != EndTrigger.Not) //满足连击条件,并且有EndTrigger条件
                {
                    BuffNext[trigger].Remove(IBuff);
                    AddEndBuff(IBuff.data.Trigger,IBuff,Action);
                }

                if (IBuff.data.StopTrigger is StopTrigger.持续 or StopTrigger.层数清空 && IBuff.data.Trigger == EndTrigger.Not)
                {
                    Action();
                    BuffNext[trigger].Remove(IBuff);
                }
                else
                {
                    AddStopEvent(IBuff.data.StopTrigger,IBuff,Action);  
                }
            }
        }
        #endregion
    }
}

