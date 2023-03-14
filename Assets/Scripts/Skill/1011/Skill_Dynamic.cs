using System.Collections;
using System.Collections.Generic;
using ARPG;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 活力四射
    /// </summary>
    public class Skill_Dynamic : PlayerSkill
    {
        public override void Play()
        {
           
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_2");
            base.Play();
            
            foreach (var buffID in data.SkillBUFF)
            {
                BuffData buffData = ConfigSystem.Instance.GetBUFFData(buffID.ToString());
                Player.AddBuff(buffData);
            }
        }
    }
}

