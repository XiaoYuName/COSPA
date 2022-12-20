using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG
{
    public abstract class Skill
    {
        protected Character Player;
        protected SkillItem data;

        public virtual void Init(Character character, SkillItem item)
        {
            Player = character;
            data = item;
        }

        public abstract void Play();
    }

    /// <summary>
    /// 战士职业普通攻击
    /// </summary>
    public class SkillSoldierAttack : Skill
    {
        public override void Play()
        {
            Debug.Log("战士职业普通攻击");
        }
    }
    
    /// <summary>
    /// 魔法师职业普通攻击
    /// </summary>
    public class SkillConjurerAttack : Skill
    {
        public override void Play()
        {
            Debug.Log("魔法师职业普通攻击");
        }
    }
}

