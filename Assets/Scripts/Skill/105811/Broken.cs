using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.BasePool;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 破碎斩
    /// </summary>
    public class Broken : PlayerSkill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,PlaySkill);
            
        }

        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_4");
            SkillPoolManager.Instance.StartCoroutine(WaitVideo());
            base.Play();
        }

        public void PlaySkill(string EventName)
        {
            if(EventName != "Broken")return;
            //1.播放截屏屏幕破碎效果
            try
            {
                Collider2D[] targets = new Collider2D[30];
                var size = Physics2D.OverlapCircleNonAlloc(Player.body.position, data.Radius, targets, data.Mask);
                Vector3[] Bounds = new Vector3[size];
                IDamage[] Enemys = new IDamage[size];
                for (int i = 0; i < size; i++)
                {
                    //获取最近的碰撞点列表
                    Bounds[i] = targets[i].bounds.ClosestPoint(Player.body.position);
                    Enemys[i] = targets[i].transform.parent.GetComponent<IDamage>();
                }
                GameManager.Instance.OptionAllDamage(Player,Enemys,data,Bounds);
                
                SceenDestruction.Instance.Destruction();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        public IEnumerator WaitVideo()
        {
            VideoManager.Instance.PlayerAvVideo(data.VideoAsset);
            Player.anim.SetFloat("GlobalSpeed",0);
            yield return new WaitForSecondsRealtime(1.9f);
            Player.anim.SetFloat("GlobalSpeed",1);
            
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,PlaySkill);
        }
    }
}

