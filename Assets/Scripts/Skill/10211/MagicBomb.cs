using System;
using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 魔法爆炸：
    ///     对300px范围内的敌人造成魔法爆炸
    /// </summary>
    public class MagicBomb : PlayerSkill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2S.EventMsg,AniamtorMsg);
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
            if (EventName != "MagicBomb") return;
            
            try
            {
                Collider2D[] targets = new Collider2D[30];
                var size = Physics2D.OverlapCircleNonAlloc(Player.body.position, data.Radius, targets, data.Mask);
                if(size <= 0)return;
                Vector3[] Bounds = new Vector3[size];
                IDamage[] Enemys = new IDamage[size];
                for (int i = 0; i < size; i++)
                {
                    //获取最近的碰撞点列表
                    Bounds[i] = targets[i].bounds.ClosestPoint(Player.body.position);
                    Enemys[i] = targets[i].transform.GetComponent<IDamage>();
                    GameObject Obj =  SkillPoolManager.Release(data.Pools[0].prefab, Bounds[i], Quaternion.identity);
                    WaitUtils.WaitTimeDo(1.2f, ()=>Obj.gameObject.SetActive(false));
                }
                
                GameManager.Instance.OptionAllDamage(Player,Enemys,data,Bounds);
            }catch (Exception)
            {
                // ignored
            }
            
        }
        
    }
}

