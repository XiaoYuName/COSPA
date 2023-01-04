using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 治愈魔法
    /// </summary>
    public class HealingMagic : PlayerSkill
    {
        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_2");
            GameManager.Instance.OptionDamage(Player,null,data,Player.transform.position);
            base.Play();
        }
    }
}

