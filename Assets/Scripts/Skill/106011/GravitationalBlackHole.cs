using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.BasePool;
using ARPG.Config;
using ARPG.Pool.Skill;
using RenderHeads.Media.AVProVideo;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 引力黑洞:
    ///     向前方发射一个黑洞,黑洞会吸收沿途范围内敌人到中心,在短暂的引导后造成爆炸
    /// </summary>
    public class GravitationalBlackHole : PlayerSkill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AniamtorMsg);
        }

        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_3");
            base.Play();
            VideoManager.Instance.StartCoroutine(WaitVideo());
        }
        
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }

            if (EventName != "GravitationalBblackHole") return;
            PlayFx();
        }

        public IEnumerator WaitVideo()
        {
            VideoManager.Instance.PlayerAvVideo(data.VideoID);
            Player.anim.SetFloat("GlobalSpeed",0);
            yield return new WaitForSecondsRealtime(1.9f);
            Player.anim.SetFloat("GlobalSpeed",1);
            
        }

        public void PlayFx()
        {
            GravitationalBlackHoleFx fx = SkillPoolManager.Release(data.Pools[0].prefab, Player.GetPoint(), Player.transform.rotation)
                .GetComponent<GravitationalBlackHoleFx>();
            fx.Play(Player,data);
        }

    }
}

