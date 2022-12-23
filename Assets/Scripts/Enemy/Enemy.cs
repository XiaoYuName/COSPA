using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
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
            Spine.skeletonDataAsset = data.SpineAsset;
            Spine.Initialize(true);
            Spine.GetComponent<MeshRenderer>().sortingOrder = sort;
            //TODO: 开始进入FSM状态
            anim.runtimeAnimatorController = data.Animator;
            SwitchFSM(FSMType.IdleFSM);
        }

        protected void Update()
        {
            FSM?.BehaviourUpdate(this);
        }

        public void SwitchFSM(FSMType type)
        {
            string ClassName = "ARPG."+type;
            animState = (int)type;
            var Type = System.Type.GetType(ClassName);
            if (Type == null)
            {
                Debug.LogError("没有对应FSM状态行为脚本,请确保命名规范");
                return;
            }
            Object Obj = Activator.CreateInstance(Type);
            if (FSM != null)
                FSM.BehaviourEnd(this);
            FSM = Obj as FSMBehaviour;
            anim.SetInteger(s_State,animState);
            if (FSM != null) FSM.BehaviourStart(this);
            
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
           SwitchFSM(FSMType.DamageFSM);
        }
    }
}

