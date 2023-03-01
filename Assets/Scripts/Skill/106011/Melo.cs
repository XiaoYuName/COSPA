using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 连锁闪电
    /// </summary>
    public class Melo : PlayerSkill
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
            if (EventName != "OpenMelo") return;
            GameManager.Instance.StartCoroutine(PlaySkill());
        }

        public IEnumerator PlaySkill()
        {
            Collider2D[] target = new Collider2D[5];
            Transform CharacterTran = Player.GetPoint("weaponMain_away").transform;
            int size = Physics2D.OverlapCircleNonAlloc(CharacterTran.position, data.Radius, target,
                data.Mask);
            if (size == 0) yield break;
            GameObject LoopFx = SkillPoolManager.Release(data.Pools[0].prefab, CharacterTran.transform.position,
                Quaternion.identity);
            // for (int i = 0; i < size; i++)
            // {
            //     while (Vector2.Distance(LoopFx,target[]))
            //     {
            //         
            //     }
            // }
        }
    }
}

