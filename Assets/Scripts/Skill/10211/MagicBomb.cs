using System.Collections;
using System.Collections.Generic;
using ARPG;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 魔法爆炸：
    ///     对300px范围内的敌人造成魔法爆炸
    /// </summary>
    public class MagicBomb : PlayerSkill
    {
        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_1");
        }
    }
}

