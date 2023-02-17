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
                GameManager.Instance.StartCoroutine(OpenMeloFx());
            }
            if (EventName == "CloseMelo")
            {
                Debug.Log("开始准备关闭");
                GameManager.Instance.StopAllCoroutines();
            }
        }

        public IEnumerator OpenMeloFx()
        {
            Vector3 StarPoint = new Vector3(Player.transform.position.x,Player.transform.position.y+7);
            while (Player.gameObject.activeSelf)
            {
                //1.获取随机位置
                Vector3 randomPos = new Vector3(StarPoint.x + Random.Range(-data.RadiusOffset.x, data.RadiusOffset.x),
                    StarPoint.y + Random.Range(-data.RadiusOffset.y, data.RadiusOffset.y));

                SkillPoolManager.Release(data.Pools[0].prefab, randomPos, Quaternion.identity);
                yield return new WaitForSeconds(data.ReleaseTime);
            }
        }
    }
}

