using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class Lunchtime : PlayerSkill
    {
        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_2");
            base.Play();
            GameManager.Instance.OptionDamage(Player,null,data,Player.transform.position);
        }
    }
}

