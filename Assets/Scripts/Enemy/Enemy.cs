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
    public class Enemy : MonoBehaviour,IDamage
    {
        /// <summary>
        /// Spine 动画
        /// </summary>
        protected SkeletonMecanim Spine;
        /// <summary>
        /// 动画控制器
        /// </summary>
        [HideInInspector]public Animator anim;
        protected int animState = 0; 
        /// <summary>
        /// 数据
        /// </summary>
        public EnemyData data;
        /// <summary>
        /// 属性
        /// </summary>
        protected CharacterState State;
        protected FSMBehaviour FSM;
        [HideInInspector]public Rigidbody2D rb;
        private static readonly int s_State = Animator.StringToHash("State");
        public Dictionary<SkillType, Skill> SkillDic = new Dictionary<SkillType, Skill>();
        protected void Awake()
        {
            anim = transform.Find("Spine").GetComponent<Animator>();
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Init(int sort,EnemyData Data)
        {
            data = Data;
            State = data.State;
            State = data.State.Clone() as CharacterState;
            Spine.GetComponent<MeshRenderer>().sortingOrder = sort;
            anim.runtimeAnimatorController = data.Animator;
            CreateSkillClass();
            //1.如果是BOSS类型敌人入场后直接进行攻击状态
            SwitchFSM(data.Type == EnemyType.BOSS ? FSMType.AttackFSM : FSMType.IdleFSM);
        }
        
        private void CreateSkillClass()
        {
            SkillDic.Clear();
            for (int i = 0; i < data.SkillTable.Count; i++)
            {
                if(String.IsNullOrEmpty(data.SkillTable[i].SkillID))continue;
                SkillItem skillItem = GameSystem.Instance.GetSkill(data.SkillTable[i].SkillID);
                Type type = Type.GetType("ARPG." +skillItem.ID);
                if (type == null) return;
                Skill skill = Activator.CreateInstance(type) as Skill;
                SkillDic.Add(data.SkillTable[i].Type,skill);
                if (skill != null) skill.Init(this, skillItem);
            }
        }
        

        protected void Update()
        {
            FSM?.BehaviourUpdate(this);
        }

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
            var Type = System.Type.GetType(ClassName);
            if (Type == null)
            {
                Debug.LogError("没有对应FSM状态行为脚本,请确保命名规范");
                return;
            }
            Object Obj = Activator.CreateInstance(Type);
            FSM?.BehaviourEnd(this);
            FSM = Obj as FSMBehaviour;
            if (anim != null)
                anim.SetInteger(s_State,animState);
            FSM?.BehaviourStart(this);

        }

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

        public CharacterState GetState()
        {
            return State;
        }

        public void IDamage(int Damage)
        {
            State.HP -= Damage;
            SwitchFSM(FSMType.DamageFSM);
        }
    }
}

