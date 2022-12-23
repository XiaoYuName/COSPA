using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class IdleFSM : FSMBehaviour
    {
        public override void BehaviourStart(Enemy enemy)
        {
            WaitUtils.WaitTimeDo(2f, delegate { enemy.SwitchFSM(FSMType.PatrolFSM); });
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


