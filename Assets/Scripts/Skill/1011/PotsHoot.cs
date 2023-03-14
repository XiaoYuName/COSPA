using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    public class PotsHoot : PlayerSkill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AniamtorMsg);
        }

        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_1");
            base.Play();
        }
        
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }
            if (EventName != "PotsHoot") return;
            Vector3 Pos = new Vector3(Player.transform.position.x + data.RadiusOffset.x,
                Player.transform.position.y + data.RadiusOffset.y);
            ParticleSystem root = SkillPoolManager.Release(data.Pools[0].prefab, Pos, Player.transform.rotation).GetComponentInChildren<ParticleSystem>();
            root.Play();
            Charm_Fx fx = root.transform.GetComponentInChildren<Charm_Fx>();
            fx.Init(Player,data);
        }
     

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
    } 
}

