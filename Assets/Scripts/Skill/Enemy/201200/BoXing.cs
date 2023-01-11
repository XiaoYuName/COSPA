using System;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 拳击
    /// </summary>
    public class BoXing : EnemySkill
    {
        private Transform AttackPoint;
        private Action Endaction;
        public override void Init(Enemy enemy, SkillItem item)
        {
            base.Init(enemy, item);
            MessageManager.Instance.Register<string>(C2C.BOSSEventMsg,AnimatorEvent);
            AttackPoint = enemy.transform.Find("AttackPoint");
        }

        public override void Play(Action action)
        {
            base.Play(action);
            Endaction = action;
            Enemy.anim.SetTrigger("Attack");
        }

        private void AnimatorEvent(string Evernt)
        {
            if (!Evernt.Equals("BossAttack")) return; 
            Endaction?.Invoke();
           Collider2D other  = Physics2D.OverlapCircle(AttackPoint.position, data.Radius,data.Mask);
           if (other == null || !other.CompareTag("Character")) return;
           IDamage target = other.GetComponentInParent<Character>();
           Vector3 attackPoint = other.bounds.ClosestPoint(AttackPoint.position);
           GameManager.Instance.OptionDamage(Enemy,target,data,attackPoint);
           
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.BOSSEventMsg,AnimatorEvent);
        }
    }
    
    
}

