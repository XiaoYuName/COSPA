using System;
using System.Collections;
using UnityEngine;

namespace ARPG
{
    public class EnemySkill:Skill
    {
        /// <summary>
        /// 该技能是否在CD中
        /// </summary>
        public bool isTimeCD;
        
        /// <summary>
        /// 释放技能
        /// </summary>
        public override void Play()
        {
            Enemy.StartCoroutine(WaitSkillTime(data.CD));
        }
        
        /// <summary>
        /// 释放技能(自动)
        /// </summary>
        /// <param name="action">释放技能后的回调</param>
        public virtual void Play(Action action)
        {
            Enemy.StartCoroutine(WaitSkillTime(data.CD));
        }
        
        
        /// <summary>
        /// 技能冷却计时
        /// </summary>
        /// <param name="skillCd"></param>
        /// <returns></returns>
        protected IEnumerator WaitSkillTime(float skillCd)
        {
            isTimeCD = true;
            yield return new WaitForSeconds(skillCd);
            isTimeCD = false;
        }

        /// <summary>
        /// 技能Skill检测
        /// </summary>
        /// <returns></returns>
        public virtual bool SkillCheck(Enemy enemy,Vector3 tagretPos)
        {
            return Vector2.Distance(enemy.transform.position, tagretPos) < data.Radius;
        }

        /// <summary>
        /// 编辑器内绘制技能范围
        /// </summary>
        public virtual void OnGizmosRadius()
        {
            if(Enemy!= null && Enemy.gameObject.activeSelf)
                Gizmos.DrawWireSphere(Enemy.GetPoint(),data.Radius);
        }
    }
}