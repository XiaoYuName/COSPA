using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 弓箭手射箭
    /// </summary>
    public class Archery : Skill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AnimatorMsg);
        }
        public override void Play()
        {
            if(Player.animSpeed == 0)return;
            Player.anim.SetTrigger("Attack");
        }
        public void AnimatorMsg(string EventName)
        {
            if (EventName != "Archery") return;
            GameObject fX =  SkillPoolManager.Release(data.Pools[0].prefab, Player.GetPoint("Body").position, Quaternion.identity);
           
        }
        
        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AnimatorMsg);
        }
        
        
    }
}

