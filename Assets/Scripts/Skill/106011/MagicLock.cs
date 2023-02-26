using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 魔法锁定:
    ///     引导一段时间，锁定自身范围内3名敌人造成黑魔法爆炸
    /// </summary>
    public class MagicLock : PlayerSkill
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
            AniamtorMsg("MagicLock");
        }
        
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }

            if (EventName != "MagicLock") return;
            SkillPoolManager.Instance.StartCoroutine(PlayFx());

        }

        public IEnumerator PlayFx()
        {
            Collider2D[] targets = new Collider2D[(int)data.Duration];
            var size = Physics2D.OverlapCircleNonAlloc(Player.transform.position, data.Radius, targets, data.Mask);
            for (int i = 0; i < size; i++)
            {
                Debug.Log("锁定目标: "+targets[i].gameObject.name+"  目标状态: "+targets[i].gameObject.activeSelf);
                SkillPoolManager.Release(data.Pools[0].prefab, targets[i].gameObject.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(data.ReleaseTime);
                GameManager.Instance.OptionDamage(Player,targets[i].gameObject.transform.GetComponentInParent<Enemy>(),data,targets[i].gameObject.transform.position);
            }
        }
    }
}

