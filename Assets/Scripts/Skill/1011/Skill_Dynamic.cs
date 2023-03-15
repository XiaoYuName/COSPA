using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 活力四射
    /// </summary>
    public class Skill_Dynamic : PlayerSkill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AniamtorMsg);
        }
        
        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_2");
            base.Play();
        }
        
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }

            if (EventName != "Skill_Dynamic") return;


            Vector3 pos = new Vector3(Player.transform.position.x + data.RadiusOffset.x,
                Player.transform.position.y + data.RadiusOffset.y);
            ParticleSystem particleSystem= SkillPoolManager.Release(data.Pools[0].prefab, pos, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
            foreach (var buffID in data.SkillBUFF)
            {
                BuffData buffData = ConfigSystem.Instance.GetBUFFData(buffID.ToString());
                Player.AddBuff(buffData);
            }

            BUFFManager.Instance.StartCoroutine(WaitShowFx(particleSystem));

        }

        public IEnumerator WaitShowFx(ParticleSystem particleSystem)
        {
            particleSystem.Play();
            float tiem = 0;
            while (tiem <= 15)
            {
                tiem += Time.deltaTime;
                Vector3 pos = new Vector3(Player.transform.position.x + data.RadiusOffset.x,
                    Player.transform.position.y + data.RadiusOffset.y);
                particleSystem.transform.position = pos;
                yield return null;
            }
            particleSystem.Stop();
            particleSystem.gameObject.SetActive(false);
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
    }
}

