using UnityEngine;
using System;
using ARPG.Pool.Skill;

namespace ARPG
{
    public class Enemy_Bait : EnemySkill
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
            if (!Evernt.Equals("Enemy_Bait")) return;
            if (Enemy == null) return;
            float rotationY = Enemy.transform.rotation.eulerAngles.y > 0 ? 0:-180;
            Vector3 pos = new Vector3(AttackPoint.position.x + data.RadiusOffset.x, AttackPoint.position.y + data.RadiusOffset.y,0);
            Collider2D target = Physics2D.OverlapCircle(pos, data.Radius, data.Mask);
            SkillPoolManager.Release(data.Pools[0].prefab, pos,Quaternion.Euler(0,rotationY,0));
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
