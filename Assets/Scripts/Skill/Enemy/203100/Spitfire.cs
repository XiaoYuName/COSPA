using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    public class Spitfire : EnemySkill
    {
        private Transform AttackPoint;
        private Action _action;
        public override void Init(Enemy enemy, SkillItem item)
        {
            base.Init(enemy, item);
            MessageManager.Instance.Register<string>(C2C.BOSSEventMsg,AnimatorEvent);
            AttackPoint = enemy.transform.Find("AttackPoint");
        }
        public override void Play(Action action)
        {
            base.Play(action);
            Enemy.anim.SetTrigger("Attack");
            _action = action;
        }
        
        private void AnimatorEvent(string Evernt)
        {
            if (!Evernt.Equals("Spitfire")) return;
            if (Enemy == null) return;
            Collider2D target = Physics2D.OverlapCircle(AttackPoint.localPosition, data.Radius, data.Mask);
            SkillPoolManager.Release(data.Pools[0].prefab, AttackPoint.position,Quaternion.Euler(
                Enemy.transform.eulerAngles.x,-Enemy.transform.eulerAngles.y,0));
            if (target != null && target.CompareTag("Character"))
            {
                IDamage damage = target.transform.GetComponentInParent<Character>();
                if (damage != null)
                {
                    GameManager.Instance.OptionDamage(Enemy,damage,data,target.bounds.ClosestPoint(AttackPoint.position));
                }
            }
            _action?.Invoke();

        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.BOSSEventMsg,AnimatorEvent);
        }
    }  
}

