using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using Spine.Unity;
using UnityEngine;

namespace ARPG
{
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// Spine 动画
        /// </summary>
        protected SkeletonMecanim Spine;
        /// <summary>
        /// 动画控制器
        /// </summary>
        protected Animator anim;
        /// <summary>
        /// 数据
        /// </summary>
        protected EnemyData data;
        /// <summary>
        /// 属性
        /// </summary>
        protected CharacterState State;

        protected void Awake()
        {
            anim = transform.Find("Spine").GetComponent<Animator>();
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
        }

        public virtual void Init(EnemyData Data)
        {
            data = Data;
            State = data.State;
            Spine.skeletonDataAsset = data.SpineAsset;
            Spine.Initialize(true);
            //TODO: 开始进入FSM状态
        }

    }
}

