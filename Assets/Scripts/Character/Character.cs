using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace ARPG
{
    public  class Character : MonoBehaviour,IDamage
    {
        /// <summary>
        /// 在背包的数据
        /// </summary>
        [HideInInspector] public CharacterBag currentBag;
        protected CharacterState State;
        private TextMeshPro NameTextUI;
        protected CharacterConfigInfo data;
        protected SkeletonMecanim Spine;
        [HideInInspector]public Animator anim;
        [HideInInspector]public AttackButton attackButton;
        [HideInInspector]public float animSpeed = 1; //动画驱动的移动速度，该速度控制在动画播放过程中,能否能进行重复操作，或者切换动画
        [HideInInspector]public bool isAI;



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
        private Dictionary<SkillType, Skill> SkillDic = new Dictionary<SkillType, Skill>();

        [HideInInspector]public Transform body;
        private static readonly int s_Die = Animator.StringToHash("Die");
        private static readonly int s_Damage = Animator.StringToHash("Damage");


        private void Awake()
        {
            NameTextUI = transform.Find("CharacterName").GetComponent<TextMeshPro>();
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
            anim = Spine.GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            Joystick = UISystem.Instance.GetNotBaseUI<DynamicJoystick>("DynamicJoystick");
            Joystick.gameObject.SetActive(true);
            attackButton = UISystem.Instance.GetUI<AttackButton>("AttackButton");
        }

        public void Init(CharacterBag bag)
        {
            isAI = false;
            currentBag = bag;
            data = InventoryManager.Instance.GetCharacter(bag.ID);
            State = bag.CurrentCharacterState.Clone() as CharacterState;
            NameTextUI.text = data.CharacterName;
            Spine.skeletonDataAsset = data.SpineAsset;
            Spine.Initialize(true);
            attackButton.InitBindButton(Attack,Skill_1,Skill_2,Skill_3);
            anim.runtimeAnimatorController = data.AnimatorController;
            body = transform.Find("Spine/SkeletonUtility-SkeletonRoot/root");
            CreateSkillClass();
        }

        private void CreateSkillClass()
        {
            SkillDic.Clear();
            for (int i = 0; i < data.SkillTable.Length; i++)
            {
                if(String.IsNullOrEmpty(data.SkillTable[i].SkillID))continue;
                SkillItem skillItem = GameSystem.Instance.GetSkill(data.SkillTable[i].SkillID);
                Type type = Type.GetType("ARPG." +skillItem.ID);
                if (type == null) return;
                Skill skill = Activator.CreateInstance(type) as Skill;
                SkillDic.Add(data.SkillTable[i].Type,skill);
                if (skill != null) skill.Init(this, skillItem);
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
                NameTextUI.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }

        private void SwitchAnimation()
        {
            if(!isAI)
                anim.SetBool(s_IsMovenemt,!(InputSpeed == Vector2.zero));
        }

        
        protected  void Attack()
        {
            if(animSpeed == 0)return;
            anim.SetTrigger(s_Attack);
            SkillDic[SkillType.Attack].Play();
        }

        protected  void Skill_1()
        {
            SkillDic[SkillType.Skill_01].Play();
        }

        protected  void Skill_2()
        {
            
        }

        protected  void Skill_3()
        {
            
        }

        //----------------------------攻击接口---------------------------//
        public CharacterState GetState()
        {
            return State;
        }
        

        public void IDamage(int Damage)
        {
            State.HP -= Damage;
            if (State.HP <= 0)
            {
                State.HP = 0;
                anim.SetTrigger(s_Die);
                isAI = true;
                rb.velocity = Vector2.zero;
                GameManager.Instance.GameOverScnen();
                return;
            }
            anim.SetTrigger(s_Damage);
        }
    }
}

