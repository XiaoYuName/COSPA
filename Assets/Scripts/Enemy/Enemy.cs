using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using Spine;
using Spine.Unity;
using UnityEngine;
using Object = System.Object;

namespace ARPG
{
    public class Enemy : MonoBehaviour,IDamage,IBuffLogic
    {
        #region PropComonent
        /// <summary>
        /// Spine 动画
        /// </summary>
        protected SkeletonMecanim Spine;
        /// <summary>
        /// 动画控制器
        /// </summary>
        [HideInInspector]public Animator anim;
        [HideInInspector]public int animState = 0; 
        /// <summary>
        /// 数据
        /// </summary>
        public EnemyData data;
        /// <summary>
        /// 属性
        /// </summary>
        protected CharacterState State;
        /// <summary>
        /// FSM 状态机
        /// </summary>
        protected FSMBehaviour FSM;
        [HideInInspector]public Rigidbody2D rb;
        private static readonly int s_State = Animator.StringToHash("State");
        /// <summary>
        /// 技能配置字典
        /// </summary>
        public Dictionary<SkillType, EnemySkill> SkillDic = new Dictionary<SkillType, EnemySkill>();
        //受击Collider
        [HideInInspector]public Collider2D DamageCollider2D;
        
        [HideInInspector]public BossStateUI stateUI;
        private SkeletonUtilityBone[] bones;
      

        /// <summary>
        /// 自定义中心点
        /// </summary>
        private Transform CentenPoint;
        protected void Awake()
        {
            anim = transform.Find("Spine").GetComponent<Animator>();
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
            rb = GetComponent<Rigidbody2D>();
            DamageCollider2D = transform.Find("DamageCollider").GetComponent<Collider2D>();
            CentenPoint = transform.Find("GetPoint");
        }

        public virtual void Init(int sort,EnemyData Data,int level)
        {
            data = Data;
            State = data.State.Clone() as CharacterState;
            State = Settings.GetLevelGrowthState(level, data.State);
            State.currentHp = State.HP;
            Spine.GetComponent<MeshRenderer>().sortingOrder = sort;
            anim.runtimeAnimatorController = data.Animator;

            bones = transform.GetComponentsInChildren<SkeletonUtilityBone>();
            CreateSkillClass();
            //1.如果是BOSS类型敌人入场后直接进行攻击状态
            if (data.Type == EnemyType.BOSS)
            {
                UISystem.Instance.OpenUI("BOSSAppear");
                AudioManager.Instance.PlayAudio("DeftualBOSS_BGM");
                stateUI = UISystem.Instance.GetUI<BossStateUI>("BossStateUI");
                stateUI.InitData(this,State);
                animState = 999;
                anim.SetInteger(s_State,animState);
                SwitchFSM(FSMType.BOSSBehaviour);
                return;
            }

            SwitchFSM(FSMType.IdleFSM);
        }
        
        #endregion

        #region Skill
        /// <summary>
        /// 创建技能Skill对象和映射字典
        /// </summary>
        private void CreateSkillClass()
        {
            SkillDic.Clear();
            for (int i = 0; i < data.SkillTable.Count; i++)
            {
                if(String.IsNullOrEmpty(data.SkillTable[i].SkillID))continue;
                SkillItem skillItem = GameSystem.Instance.GetSkill(data.SkillTable[i].SkillID);
                Type type = Type.GetType("ARPG." +skillItem.ID);
                if (type == null) return;
                EnemySkill skill = Activator.CreateInstance(type) as EnemySkill;
                SkillDic.Add(data.SkillTable[i].Type,skill);
                if (skill != null) skill.Init(this, skillItem);
            }
        }
        #endregion
        
        #region FSM
        protected void Update()
        {
            FSM?.BehaviourUpdate(this);
        }

        /// <summary>
        /// 切换FSM 状态
        /// </summary>
        /// <param name="type">状态类型</param>
        public void SwitchFSM(FSMType type)
        {
            if (type == FSMType.Note)
            {
                FSM?.BehaviourEnd(this);
                FSM = null;
                return;
            }
            string ClassName = "ARPG."+type;
            animState = (int)type;
            if (type== FSMType.AttackFSM && data.Type == EnemyType.BOSS)
            {
                ClassName = "ARPG."+"BOSS"+type;
                animState = (int)FSMType.AttackFSM;
            }
            var Type = System.Type.GetType(ClassName);
            if (Type == null)
            {
                Debug.LogError("没有对应FSM状态行为脚本,请确保命名规范 :"+ClassName);
                return;
            }
            Object Obj = Activator.CreateInstance(Type);
            FSM?.BehaviourEnd(this);
            FSM = Obj as FSMBehaviour;
            if (anim != null && !ClassName.Contains("BOSS"))
                anim.SetInteger(s_State,animState);
            FSM?.BehaviourStart(this);

        }

        /// <summary>
        /// 退出FSM 状态
        /// </summary>
        public void QuitFSM()
        {
            SwitchFSM(FSMType.Note);
            FSM = null;
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            FSM.OnColliderEnter2D(other,this);
        }

        public void OnCollisionExit2D(Collision2D other)
        {
            FSM.OnColliderExit2D(other,this);
        }
        #endregion

        #region IDamage
        
        /// <summary>
        /// 获取自身属性状态
        /// </summary>
        /// <returns></returns>
        public CharacterState GetState()
        {
            return State;
        }

        /// <summary>
        /// 获取自身位置
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPoint()
        {
            return CentenPoint.position;
        }
        
        /// <summary>
        /// 获取骨骼位置,该位置跟随Spine动画位置
        /// </summary>
        /// <param name="BoneName">骨骼</param>
        /// <returns>返回对应骨骼根节点位置，如果找不到,则直接返回Character原点</returns>
        public Transform GetPoint(string BoneName)
        {
            if (bones == null) return transform;
            foreach (var bone in bones)
            {
                if (bone.bone.ToString() == BoneName)
                {
#if UNITY_EDITOR
                    Debug.Log("找到对应:"+BoneName+"骨骼跟随节点");
#endif
                    return bone.transform;
                }
            }
            return transform;
        }
        
        /// <summary>
        /// 获取自身Ttransfom 组件
        /// </summary>
        /// <returns></returns>
        public Transform GetTransform()
        {
            return CentenPoint;
        }
        
        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="Damage">受到伤害点数</param>
        public void IDamage(int Damage)
        {
            State.currentHp -= Damage;
            SwitchFSM(FSMType.DamageFSM);
        }
        
        /// <summary>
        /// 回复血量
        /// </summary>
        /// <param name="Reply">回复值</param>
        public void IReply(int Reply)
        {
            State.currentHp = Mathf.Min(State.currentHp+Reply, State.HP);
        }
        #endregion

        #region IBUFF
        //----------------------------BUFF--------------------------------//
        public IBuffLogic GetBuffLogic()
        {
            return this;
        }
        public IDamage GetIDamage()
        {
            return this;
        }

        public BuffStateUI GetStateUI()
        {
            return null;
        }
        
        public void AddBuff(BuffData buff)
        {
            
        }
        public void AddBuffEvent(EndTrigger trigger, IBuff IBuff, Action action)
        {
            
        }
        #endregion
    }
}

