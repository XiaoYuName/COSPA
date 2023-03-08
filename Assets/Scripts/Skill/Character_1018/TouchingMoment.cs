using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.BasePool;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;


namespace ARPG
{
    /// <summary>
    /// 心动时刻
    /// </summary>
    public class TouchingMoment : PlayerSkill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AniamtorMsg);
        }

        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_4");
            SkillPoolManager.Instance.StartCoroutine(WaitVideo());
            base.Play();
        }
        public IEnumerator WaitVideo()
        {
            VideoManager.Instance.PlayerAvVideo(data.VideoAsset);
            Player.anim.SetFloat("GlobalSpeed",0);
            yield return new WaitForSecondsRealtime(1.9f);
            Player.anim.SetFloat("GlobalSpeed",1);
        }
        
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }
            if (EventName != "TouchingMoment") return;
            Vector3 zeroPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f,0.75f,0));
            zeroPoint.z = 0;
            ParticleSystem Root = SkillPoolManager.Release(data.Pools[0].prefab, zeroPoint, Quaternion.identity).GetComponent<ParticleSystem>();
            Root.Play();
            SkillPoolManager.Instance.StartCoroutine(WaitClose(Root));
        }

        public IEnumerator WaitClose(ParticleSystem root)
        {
            Charm_Fx fx = root.transform.GetComponentInChildren<Charm_Fx>();
            fx.Init(Player,data);
            yield return null;
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
    }
}

