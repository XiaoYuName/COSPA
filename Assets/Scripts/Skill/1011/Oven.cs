using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 火焰箭
    /// </summary>
    public class Oven : PlayerSkill
    {
        ParticleSystem ShowOvenLoop = null;
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
    
            if (EventName != "ShowOven" && EventName != "Oven") return;
           
            if (EventName == "ShowOven")
            {
                //开始显示蓄力粒子特效
                Vector3 pos = new Vector3(Player.GetPoint("Ex0001").position.x + data.PoolOffets[0].x,
                    Player.GetPoint("Ex0001").position.y + data.PoolOffets[0].y,0);
                ShowOvenLoop = SkillPoolManager.Release(data.Pools[0].prefab, pos, Quaternion.Euler(-90,Player.transform.rotation.eulerAngles.y,0)).GetComponent<ParticleSystem>();
                ShowOvenLoop.Play();
            }
            if (EventName == "Oven")
            {
                if (ShowOvenLoop != null)
                {
                    ShowOvenLoop.Stop();
                    ShowOvenLoop.gameObject.SetActive(false);
                    SkillPoolManager.Release(data.Pools[1].prefab, ShowOvenLoop.transform.position,
                        Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>().Play();
                    ShowOvenLoop = null;
                    Vector3 CrentPoint = new Vector3(Player.GetPoint("weaponMain_away").position.x + data.PoolOffets[2].x,
                        Player.GetPoint("weaponMain_away").position.y + data.PoolOffets[2].y);
                    GameObject fX =  SkillPoolManager.Release(data.Pools[2].prefab, CrentPoint, Player.transform.rotation);
                    MovForward movForward = fX.GetComponent<MovForward>();
                    movForward.PlayMovForward(Player,data,OnCollidr);
                }
            }
        }

        private void OnCollidr(Collider2D col)
        {
            ARPG.Enemy target = col.transform.GetComponentInParent<Enemy>();
            
            ParticleSystem particleSystem =  SkillPoolManager.Release(data.Pools[3].prefab, target.GetPoint("body").position, Quaternion.Euler(-90, 0, 0)).GetComponent<ParticleSystem>();
           particleSystem.Play();
        }

        public IEnumerator WaitVideo()
        {
            VideoManager.Instance.PlayerAvVideo(data.VideoAsset);
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
