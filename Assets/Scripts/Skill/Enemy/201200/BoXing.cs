using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 拳击
    /// </summary>
    public class BoXing : EnemySkill
    {
        public override void Play(Action action)
        {
            base.Play(action);
            Enemy.anim.SetTrigger("Attack");
            Debug.Log("石头人拳击");
            WaitUtils.WaitTimeDo(2, () => action?.Invoke());
        }
    }
}

