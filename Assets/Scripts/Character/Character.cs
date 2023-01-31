using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace ARPG
{
    public  class Character : MonoBehaviour,IDamage,IBuffLogic
    {
        /// <summary>
        /// 在背包的数据
        /// </summary>
        [HideInInspector] public CharacterBag currentBag;
        protected CharacterState State;
        protected CharacterConfigInfo data;
        protected SkeletonMecanim Spine;
        [HideInInspector]public Animator anim;
        [HideInInspector]public AttackButton attackButton;
        private PlayerState StateUI;
        [HideInInspector]public float animSpeed = 1; //动画驱动的移动速度，该速度控制在动画播放过程中,能否能进行重复操作，或者切换动画
        [HideInInspector]public bool isAI;
        public Collider2D DamageCollider2D;



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

        private void Awake()
        {
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
            anim = Spine.GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            Joystick = UISystem.Instance.GetNotBaseUI<DynamicJoystick>("DynamicJoystick");
            Joystick.gameObject.SetActive(true);
            attackButton = UISystem.Instance.GetUI<AttackButton>("AttackButton");
            DamageCollider2D = transform.Find("DamageCollier").GetComponent<Collider2D>();
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
            CreateSkillClass();
            CreatBuff();
        }

        private void CreateSkillClass()
        {
            SkillDic.Clear();
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
                rb.velocity = InputSpeed.normalized * State.MovSpeed  * animSpeed* Time.fixedDeltaTime;
            }
            else
            {
                rb.velocity = Vector2.zero;
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

        /// <summary>
        /// 普攻技能回调函数
        /// </summary>
        protected  void Attack()
        {
            if(animSpeed == 0)return;
            anim.SetTrigger(s_Attack);
            SkillDic[SkillType.Attack].Play();
            BuffTriggerEvent(BuffTrigger.攻击时);
        }

        /// <summary>
        /// 技能1回调函数
        /// </summary>
        protected  void Skill_1()
        {
            if(SkillDic.ContainsKey(SkillType.Skill_01))
                SkillDic[SkillType.Skill_01].Play();
        }
    
        /// <summary>
        /// 技能2回调函数
        /// </summary>
        protected  void Skill_2()
        {
            if(SkillDic.ContainsKey(SkillType.Skill_02))
                SkillDic[SkillType.Skill_02].Play();
        }

        /// <summary>
        /// 技能3回调函数
        /// </summary>
        protected  void Skill_3()
        {
            if(SkillDic.ContainsKey(SkillType.Skill_03))
                SkillDic[SkillType.Skill_03].Play();
        }

        /// <summary>
        /// 觉醒回调函数
        /// </summary>
        protected void Skill_4()
        {
            if(SkillDic.ContainsKey(SkillType.Evolution))
                SkillDic[SkillType.Evolution].Play();
        }

        //----------------------------攻击接口---------------------------//
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
        }

        public void IReply(int Reply)
        {
            State.currentHp = Mathf.Min(State.currentHp+Reply,State.HP);
            StateUI.UpdateState(State);
        }


        //--------------------------------BUFF接口----------------------------------------//
        private void BuffTriggerEvent(BuffTrigger type)
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                Buffs[i].Trigger(type);
            }
        }

        private void CreatBuff()
        {
            for (int i = 0; i < data.deftualBuffID.Count; i++)
            {
                
                BuffData newBuff = ConfigSystem.Instance.GetBUFFData(data.deftualBuffID[i]);
                if (newBuff.behaviourType == BuffBehaviourType.光环) 
                    GetStateUI().AddBuffItemUI(newBuff.ToBuff(this));
                Buffs.Add(newBuff.ToBuff(this));
            }
        }

        public IBuffLogic GetBuffLogic()
        {
            return this;
        }

        public BuffStateUI GetStateUI()
        {
            return StateUI.GetBuffStateUI();
        }

        public IDamage GetIDamage()
        {
            return this;
        }
    }
}

