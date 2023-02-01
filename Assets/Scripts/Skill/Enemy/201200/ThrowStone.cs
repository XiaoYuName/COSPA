using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 砸石头
    /// </summary>
    public class ThrowStone : EnemySkill
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
            Enemy.anim.SetTrigger("Skill_1");
            _action = action;
        }

        private void AnimatorEvent(string Evernt)
        {
            if (!Evernt.Equals("ThrowStone")) return;
            if (Enemy == null) return;
            Transform SkillPoint = Enemy.transform.Find("SkillPoint");
            MovFxItem movFxItem =  SkillPoolManager.Release(data.Pools[0].prefab,SkillPoint.position,
                Quaternion.identity).GetComponent<MovFxItem>();
            movFxItem.StarMovTarget(Enemy,-Enemy.GetTransform().right.normalized,data,_action);
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.BOSSEventMsg,AnimatorEvent);
        }

        public override bool SkillCheck(Enemy enemy, Vector3 tagretPos)
        {
            RaycastHit2D hit = Physics2D.Raycast(enemy.GetPoint(), -enemy.GetTransform().right.normalized, data.Radius,data.Mask);
           if (hit.transform == null) return false;
           if (hit.transform.CompareTag("Character"))
           {
               return true;
           }
           return false;

        }


        public override void OnGizmosRadius()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(Enemy.GetPoint(), -Enemy.GetTransform().right.normalized * data.Radius);
        }
    }
}

