using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 冥落
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

            if (EventName != "OpenMelo" && EventName != "CloseMelo") return;

            if (EventName == "OpenMelo")
            {
                Debug.Log("开始准备砸地");
                GameManager.Instance.StartCoroutine(PlaySkill());
            }
            if (EventName == "CloseMelo")
            {
                Debug.Log("开始准备关闭");
            }
        }

        public IEnumerator PlaySkill()
        {
            for (int i = 0; i < 5; i++)
            {
                Vector3 randomPoint = new Vector3(
                    Player.transform.position.x + Random.Range(-data.RadiusOffset.x, data.RadiusOffset.x), Player.transform.position.y +
                    Random.Range(-Player.transform.position.y, Player.transform.position.y));
                _FxItem fxItem = SkillPoolManager.Release(data.Pools[0].prefab, randomPoint).GetComponent<_FxItem>();
                fxItem.Play(Player,data);
                yield return new WaitForSeconds(data.ReleaseTime);
            }
        }
    }
}

