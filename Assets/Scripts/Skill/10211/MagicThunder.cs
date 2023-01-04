
using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 引雷阵
    /// </summary>
    public class MagicThunder : PlayerSkill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2S.EventMsg,AniamtorMsg);
        }

        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_3");
            base.Play();
        }
        
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }
            switch (EventName)
            {
                case "StopVideoSkill3":
                    Player.StartCoroutine(WaitVideo());
                    break;
                case "MagicThunder":
                    Player.StartCoroutine(CrearFx());
                    break;
            }
        }

        public IEnumerator WaitVideo()
        {
            VideoManager.Instance.PlayerAvVideo(data.ID);
            yield return new WaitForSecondsRealtime(1.9f);
            Player.anim.speed = 1;
        }

        public IEnumerator CrearFx()
        {
            for (int i = 0; i < 8; i++)
            {
               Collider2D target = Physics2D.OverlapCircle(Player.body.transform.position, data.Radius, data.Mask);
               if (target != null && target.CompareTag("Character"))
               {
                   Vector3 Point = target.bounds.ClosestPoint(Player.body.position);
                   GameObject Fx = SkillPoolManager.Release(data.Pools[0].prefab, target.transform.position, Quaternion.identity);
                   GameManager.Instance.OptionDamage(Player,target.GetComponent<Enemy>(),data,Point);
                   WaitUtils.WaitTimeDo(data.Duration, () => Fx.gameObject.SetActive(false));
                   yield return new WaitForSeconds(0.25f);
               }

               
            }
        }
        
        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.Register<string>(C2S.EventMsg,AniamtorMsg);
        }
    }
}

