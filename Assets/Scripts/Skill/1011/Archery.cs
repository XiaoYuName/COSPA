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
        private int AttackCount;
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AnimatorMsg);
        }
        public override void Play()
        {
            if (AttackCount >= 3)
            {
                if(Player.animSpeed == 0)return;
                AttackCount = 0;
                Player.anim.SetTrigger("SkipAttack");
            }
            else
            {
                if(Player.animSpeed == 0)return;
                Player.anim.SetTrigger("Attack");
                AttackCount++;
            }

            
        }
        public void AnimatorMsg(string EventName)
        {
            if (EventName != "Archery") return;
            Vector3 CrentPoint = new Vector3(Player.GetPoint("weaponMain_away").position.x + data.RadiusOffset.x,
                Player.GetPoint("weaponMain_away").position.y + data.RadiusOffset.y);
            GameObject fX =  SkillPoolManager.Release(data.Pools[0].prefab, CrentPoint, Player.transform.rotation);
            MovForward movForward = fX.GetComponent<MovForward>();
            movForward.PlayMovForward(Player,data);
        }
        
        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AnimatorMsg);
        }
        
        
    }
}

