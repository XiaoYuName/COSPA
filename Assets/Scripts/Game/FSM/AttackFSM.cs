using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class AttackFSM : FSMBehaviour
    {
        private Vector3 tagretPos;
        private static readonly int s_Attack = Animator.StringToHash("Attack");
        private float valueSpeed;

        public override void BehaviourStart(Enemy enemy)
        {
            valueSpeed = Random.value;
        }

        public override void BehaviourUpdate(Enemy enemy)
        {
            tagretPos = GameManager.Instance.Player.transform.position;
            //1.判断我与玩家的位置,如果大于普通攻击范围内同时,判断自身类型,如果是普通小怪，则只有普通攻击,如果是精英怪,则随机开始进入一个特殊的技能状态直到结束,则追击到攻击范围内
            if (Vector2.Distance(enemy.transform.position, tagretPos) < enemy.data.Attackradius)
            {
                //发送攻击,TODO: 发动技能
                enemy.anim.SetTrigger(s_Attack);
            }
            else
            {
                enemy.transform.position = Vector2.Lerp(enemy.transform.position, 
                    tagretPos, enemy.GetState().MovSpeed*valueSpeed* Time.deltaTime);
                Flip(enemy);
            }
        }
        public void Flip(Enemy enemy)
        {
            enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.position.x < tagretPos.x ? 180 : 0, 0);
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
