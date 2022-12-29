using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public class Lunchtime : PlayerSkill
    {
        public override void Play()
        {
            base.Play();
            Player.anim.SetTrigger("Skill_2");
            GameManager.Instance.OptionDamage(Player,null,data,Player.transform.position);
        }
    }
}

