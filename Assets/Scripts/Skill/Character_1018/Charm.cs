using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 魅惑
    /// </summary>
    public class Charm : PlayerSkill
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
            if (EventName != "Charm") return;
            ParticleSystem Root =  SkillPoolManager.Release(data.Pools[0].prefab, Player.GetPoint("skull").position, Quaternion.identity).GetComponent<ParticleSystem>();
            SkillPoolManager.Instance.StartCoroutine(WaitClose(Root));
        }

        public IEnumerator WaitClose(ParticleSystem root)
        {
            Charm_Fx fx = root.transform.GetComponentInChildren<Charm_Fx>();
            fx.Init(Player,data);
            // yield return new WaitForSeconds(root.main.duration);
            // root.gameObject.SetActive(false);
            yield return null;
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
        
    }
}

