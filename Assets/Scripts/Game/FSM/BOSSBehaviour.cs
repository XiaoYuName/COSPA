using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// BOSS类型怪物特殊处理
    /// </summary>
    public class BOSSBehaviour : FSMBehaviour
    {
        private Enemy Base;

        public override void BehaviourStart(Enemy enemy)
        {
            Base = enemy;
            Base.anim.SetTrigger("Star");
            enemy.stateUI.InitData(enemy,enemy.GetState());
            MessageManager.Instance.Register<string>(C2C.BOSSEventMsg,SpienEvent);
        }

        public override void BehaviourUpdate(Enemy enemy)
        {
            
        }

        public override void BehaviourEnd(Enemy enemy)
        {
            MessageManager.Instance.URegister<string>(C2C.BOSSEventMsg,SpienEvent);
        }


        private void SpienEvent(string name)
        {
            if (name == "SummonStop")
            {
                Base.SwitchFSM(FSMType.IdleFSM);
            }
        }


        public override void OnColliderEnter2D(Collision2D other, Enemy enemy)
        {
            
        }

        public override void OnColliderExit2D(Collision2D other, Enemy enemy)
        {
            
        }
    }
}

