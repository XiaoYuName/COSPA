using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

namespace ARPG.Character
{
    public class Character : MonoBehaviour
    {
        public float speed;
        private Rigidbody2D rb;
        private SkeletonAnimation anim;
        private Vector2 Movspeed;
        private AnimationState _animationState;

        [SpineAnimation]
        public string animationname;

        [SpineAnimation]
        public string stopanimname;

        [SpineAnimation]
        public string Attackanimname;

        private int State;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = transform.GetComponentInChildren<SkeletonAnimation>();
            State = 0;
            SwitchAnimator(animationname, true);
        }
        
        private void Update()
        {
            Movspeed.x = Input.GetAxisRaw("Horizontal");
            Movspeed.y = Input.GetAxisRaw("Vertical");
            if (Movspeed == Vector2.zero)
            {
                if (State == 1)
                {
                    SwitchAnimator(animationname, true);
                }
                State = 0;
            }
            else
            {
                if (State == 0)
                {
                    SwitchAnimator(stopanimname, true);
                }
                State = 1;
            }
            if (Movspeed.x != 0)
            {
                transform.rotation = Quaternion.Euler(0, Movspeed.x < 0 ? 180 : 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                SwitchAnimator(Attackanimname, false);
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = Movspeed.normalized * speed * Time.fixedDeltaTime;
            
        }


        private void SwitchAnimator(string animName,bool isLoop)
        {
            anim.state.SetAnimation(0,animName, isLoop);
        }
    }
}

