using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    public class DamageFSM : FSMBehaviour
    {
        private static readonly int s_Damage = Animator.StringToHash("Damage");

        public override void BehaviourStart(Enemy enemy)
        {
            if (enemy.data.Type == EnemyType.BOSS && enemy.stateUI != null)
            {
                enemy.stateUI.UpdateSlider(enemy,enemy.GetState(),enemy.Level);
            }

            enemy.anim.SetTrigger(s_Damage);
            enemy.SwitchFSM(enemy.GetState().currentHp > 0 ? FSMType.AttackFSM : FSMType.DieFSM);
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

