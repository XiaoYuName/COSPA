using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ARPG.Pool.Skill;

namespace ARPG
{
    /// <summary>
    /// 怪物普通攻击--物理
    /// </summary>
    public class SkillEnemySoldierAttack : EnemySkill
    {
        private Transform AttackPoint;
        public override void Play(Action action)
        {
            base.Play(action);
            Enemy.StartCoroutine(PlayFx());
        }
        private IEnumerator PlayFx()
        {
            AttackPoint = Enemy.transform.Find("AttackPoint");
            if (AttackPoint == null)
            {
                AttackPoint = Enemy.transform;
            }
            yield return new WaitForSeconds(data.ReleaseTime);
            Collider2D target = Physics2D.OverlapCircle(AttackPoint.position,data.Radius,data.Mask);
            if (target != null && target.CompareTag("Character"))
            {
                IDamage player = target.transform.GetComponentInParent<Character>();
                if (player != null)
                {
                    Vector3 boundPoint = target.bounds.ClosestPoint(AttackPoint.position);
                    GameManager.Instance.OptionDamage(Enemy,player,data,boundPoint);
                }
            }
        }
    }
    /// <summary>
    /// 怪物普通攻击--魔法
    /// </summary>
    public class SkillEnemyMagicAttack : EnemySkill
    {
        private Transform AttackPoint;
        public override void Play(Action action)
        {
            base.Play(action);
            Enemy.StartCoroutine(PlayFx());
        }
        private IEnumerator PlayFx()
        {
            AttackPoint = Enemy.transform.Find("AttackPoint");
            if (AttackPoint == null)
            {
                AttackPoint = Enemy.transform;
            }
            yield return new WaitForSeconds(data.ReleaseTime);
            Collider2D target = Physics2D.OverlapCircle(AttackPoint.position,data.Radius,data.Mask);
            if (target != null && target.CompareTag("Character"))
            {
                IDamage player = target.transform.GetComponentInParent<Character>();
                if (player != null)
                {
                    Vector3 boundPoint = target.bounds.ClosestPoint(AttackPoint.position);
                    GameManager.Instance.OptionDamage(Enemy,player,data,boundPoint);
                }
            }
        }
    }
}
