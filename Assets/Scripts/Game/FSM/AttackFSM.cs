using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    public class AttackFSM : FSMBehaviour
    {
        private Vector3 tagretPos;
        private static readonly int s_Attack = Animator.StringToHash("Attack");
        private float valueSpeed;
        private float AttackCD;

        public override void BehaviourStart(Enemy enemy)
        {
            valueSpeed = Random.Range(0.7f,1f);
            AttackCD =  GameSystem.Instance.GetSkill(enemy.data.SkillTable[0].SkillID).CD;
        }

        public override void BehaviourUpdate(Enemy enemy)
        {
            Vector3 PlayePoint = GameManager.Instance.Player.transform.position;
            tagretPos = new Vector3(PlayePoint.x+enemy.data.Attackradius,PlayePoint.y+0.25f,PlayePoint.z);
            //1.判断我与玩家的位置,如果大于普通攻击范围内同时,判断自身类型,如果是普通小怪，则只有普通攻击,如果是精英怪,则随机开始进入一个特殊的技能状态直到结束,则追击到攻击范围内
            if (Vector2.Distance(enemy.transform.position, tagretPos) < enemy.data.Attackradius
                && AttackCD <= 0)
            {
                //发送攻击,TODO: 发动技能
                AttackCD =  GameSystem.Instance.GetSkill(enemy.data.SkillTable[0].SkillID).CD;
                enemy.anim.SetTrigger(s_Attack);
                enemy.SkillDic[SkillType.Attack].Play(null);
            }
            else
            {
                if(AttackCD >=0)
                    AttackCD -= Time.deltaTime;
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, tagretPos, 
                    enemy.GetState().MovSpeed*valueSpeed* Time.deltaTime);
                Flip(enemy);
            }
        }
        
        
        public void Flip(Enemy enemy)
        {
            Vector3 PlayePoint = GameManager.Instance.Player.transform.position;
            enemy.transform.rotation = Quaternion.Euler(0, enemy.transform.position.x < PlayePoint.x ? 180 : 0, 0);
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
