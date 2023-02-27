using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.BasePool;
using ARPG.Config;
using UnityEngine;

namespace ARPG
{
    public class Aurora : PlayerSkill
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
            base.Play();
            VideoManager.Instance.StartCoroutine(WaitVideo());
        }
        
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }
    
            if (EventName != "Aurora" && EventName != "EndAurora") return;
    
            if (EventName == "OpenHIght")
            {
                
            }
    
            if (EventName == "Aurora")  //爆发Tween
            {
                PostManager.Instance.StarTween(0.5f,0.5f,SkilDamage,FuncMode.Crent);
            }
    
            if (EventName == "EndAurora")
            {
                PostManager.Instance.StarTween(2f,0.25f,WaitSkillDamage,FuncMode.Star);
            }
        }

        public void SkilDamage()
        {
            Collider2D[] others = new Collider2D[20];
            int Size = Physics2D.OverlapCircleNonAlloc(Player.transform.position, data.Radius,others,data.Mask);
            if (Size <= 0) return;
            List<IDamage> targets = new List<IDamage>();
            for (int i = 0; i < Size; i++)
            {
                targets.Add(others[i].GetComponentInParent<Enemy>());
            }
            GameManager.Instance.OptionAllDamage(Player,targets.ToArray(),data,false);
        }

        public void WaitSkillDamage()
        {
            Collider2D[] others = new Collider2D[20];
            int Size = Physics2D.OverlapCircleNonAlloc(Player.transform.position, data.Radius,others,data.Mask);
            if (Size <= 0) return;
            List<IDamage> targets = new List<IDamage>();
            for (int i = 0; i < Size; i++)
            {
                targets.Add(others[i].GetComponentInParent<Enemy>());
            }
            GameManager.Instance.OptionAllDamage(Player,targets.ToArray(),data);
        }



        public IEnumerator WaitVideo()
        {
            VideoManager.Instance.PlayerAvVideo(data.VideoID);
            Player.anim.SetFloat("GlobalSpeed",0);
            yield return new WaitForSeconds(1.9f);
            Player.anim.SetFloat("GlobalSpeed",1);
                
        }
    
        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
        
    }
}

