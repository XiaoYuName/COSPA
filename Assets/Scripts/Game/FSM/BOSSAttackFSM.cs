using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    public class BOSSAttackFSM : FSMBehaviour
    {
        private Enemy Base;
        private Vector3 tagretPos;
        private float valueSpeed;

        /// <summary>
        /// 记录全部技能冷却时间
        /// </summary>
        private List<EnemySkill> SkillTime = new List<EnemySkill>();
        /// <summary>
        /// 下一次发起攻击的技能
        /// </summary>
        private EnemySkill selectSkillItem;

        private bool waitNextTime;
        private WaitForSeconds durntTime;
        
        public override void BehaviourStart(Enemy enemy)
        {
            Base = enemy;
            valueSpeed = Random.Range(0.5f,0.7f);
            InitSkill();
            selectSkillItem = Base.SkillDic[SkillType.Attack] as EnemySkill;
            waitNextTime = false;
            enemy.animState = 2;
            durntTime = new WaitForSeconds(enemy.SkillDic[SkillType.Attack].data.CD);
            Base.anim.SetInteger("State",enemy.animState);
        }

        public override void BehaviourUpdate(Enemy enemy)
        {
            Vector3 PlayePoint = GameManager.Instance.Player.transform.position;
            tagretPos = new Vector3(PlayePoint.x+enemy.data.Attackradius,PlayePoint.y+0.25f,PlayePoint.z);
            //1.判断我与玩家的位置,如果大于普通攻击范围内同时,判断自身类型,如果是普通小怪，则只有普通攻击,如果是精英怪,则随机开始进入一个特殊的技能状态直到结束,则追击到攻击范围内
            if (selectSkillItem.SkillCheck(enemy,tagretPos) && !selectSkillItem.isTimeCD && !waitNextTime)
            {
                selectSkillItem.Play(RandomSkill);
            }
            else
            {
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, tagretPos, 
                    enemy.GetState().MovSpeed*valueSpeed* Time.deltaTime);
                Flip(enemy);
            }
        }


        private void InitSkill()
        {
            foreach (var vSkill in Base.SkillDic)
            {
                SkillTime.Add(vSkill.Value as EnemySkill);
            }
        }

        /// <summary>
        /// 随机选择一个冷却不为0的技能,如果都在冷却,则下次攻击为普攻
        /// </summary>
        public void RandomSkill()
        {
            waitNextTime = true;
            GameSystem.Instance.StartCoroutine(WaitTime());
            List<EnemySkill> NotTimeSkill = SkillTime.FindAll(s => s.isTimeCD == false);
            if (NotTimeSkill.Count <= 0)
            {
                selectSkillItem = Base.SkillDic[SkillType.Attack];
                return;
            }
            int index = Random.Range(0, NotTimeSkill.Count);
            selectSkillItem = NotTimeSkill[index];
        }

        public IEnumerator WaitTime()
        {
            yield return durntTime;
            waitNextTime = false;
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

