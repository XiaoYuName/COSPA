using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
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
            Debug.Log("扔石头");
            _action?.Invoke();
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.BOSSEventMsg,AnimatorEvent);
        }
    }
}

