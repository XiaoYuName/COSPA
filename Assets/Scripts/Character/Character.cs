using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using Spine.Unity;
using TMPro;
using UnityEngine;

namespace ARPG
{
    public class Character : MonoBehaviour
    {
        private CharacterState State;
        private TextMeshPro NameTextUI;
        private CharacterConfigInfo data;
        private SkeletonMecanim Spine;
        private Animator anim;
        
        
        //--------------------------Movenemt--------------------------//
        private Rigidbody2D rb;
        /// <summary>
        /// 摇杆组件
        /// </summary>
        private DynamicJoystick Joystick;
        private Vector2 InputSpeed;
        private static readonly int s_IsMovenemt = Animator.StringToHash("isMovenemt");

        private void Awake()
        {
            NameTextUI = transform.Find("CharacterName").GetComponent<TextMeshPro>();
            Spine = transform.Find("Spine").GetComponent<SkeletonMecanim>();
            anim = Spine.GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            Joystick = UISystem.Instance.GetNotBaseUI<DynamicJoystick>("DynamicJoystick");
        }

        public void Init(CharacterBag bag)
        {
            data = InventoryManager.Instance.GetCharacter(bag.ID);
            State = bag.CurrentCharacterState;
            NameTextUI.text = data.CharacterName;
            Spine.skeletonDataAsset = data.SpineAsset;
            Spine.Initialize(true);
            anim.runtimeAnimatorController = data.AnimatorController;
        }

        private void Update()
        {
            InputSpeed.x = Joystick.Horizontal;
            InputSpeed.y = Joystick.Vertical;
            SwitchAnimation();
        }

        private void FixedUpdate()
        {
            Movenemt();
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movenemt()
        {
            if (InputSpeed != Vector2.zero)
            {
                rb.velocity = InputSpeed.normalized * State.MovSpeed * Time.fixedDeltaTime;
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
            if (InputSpeed != Vector2.zero)
            {
                transform.rotation = Quaternion.Euler(0,InputSpeed.x <0 ? 180:0,0);
            }
        }

        private void SwitchAnimation()
        {
            anim.SetBool(s_IsMovenemt,!(InputSpeed == Vector2.zero));
        }
    }
}

