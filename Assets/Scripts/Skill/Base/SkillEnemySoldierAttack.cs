using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ARPG.Pool.Skill;

namespace ARPG
{
    /// <summary>
    /// 怪物普通攻击
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
            AttackPoint = Enemy.GetPoint("body");
            yield return new WaitForSeconds(data.ReleaseTime);
            _FxItem fxItem = SkillPoolManager.Release(data.Pools[0].prefab, AttackPoint.position,
                Quaternion.identity).GetComponent<_FxItem>();
            fxItem.Play(Enemy,data);
        }
    }

}
