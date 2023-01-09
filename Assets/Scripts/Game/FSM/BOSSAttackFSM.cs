using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class BOSSAttackFSM : FSMBehaviour
    {
        private Enemy Base;
        private Vector3 tagretPos;
        private float valueSpeed;
        
        public override void BehaviourStart(Enemy enemy)
        {
            Base = enemy;
            valueSpeed = Random.Range(0.7f,1f);
            Debug.Log("进入BOSSAttack状态");
        }

        public override void BehaviourUpdate(Enemy enemy)
        {
            
        }

        public override void BehaviourEnd(Enemy enemy)
        {
           
        }

        public override void OnColliderEnter2D(Collision2D other, Enemy enemy)
        {
            
        }

        public override void OnColliderExit2D(Collision2D other, Enemy enemy)
        {
           
        }
    }
}

