using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 烈焰焚烬
    /// </summary>
    public class FlameBurns : PlayerSkill
    {
        private List<Enemy> targets;
        private List<GameObject> FxFlame;
        private Coroutine flameCoroutine;
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AniamtorMsg);
             targets = new List<Enemy>();
             FxFlame = new List<GameObject>();
        }

        public override void Play()
        {
            if(isCold || Player.animSpeed ==0)return;
            Player.anim.SetTrigger("Skill_4");
            base.Play();
        }
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }

            if (EventName == "RunFlame")
            {
                flameCoroutine = Player.StartCoroutine(FlameBuff());
            }

            if (EventName == "FlameBomb")
            {
                if(flameCoroutine != null)
                    Player.StopCoroutine(flameCoroutine);
                Player.StartCoroutine(FlameBomb());
            }
        }


        /// <summary>
        /// 给敌方挂载烈焰buff
        /// </summary>
        /// <returns></returns>
        public IEnumerator FlameBuff()
        {
            targets.Clear();
            FxFlame.Clear();
            while (true)
            {
                Collider2D target = Physics2D.OverlapCircle(Player.body.transform.position, data.Radius, data.Mask);
                if (target != null && target.CompareTag("Character"))
                {
                    ARPG.Enemy tarEnemy = target.transform.parent.GetComponent<Enemy>();
                    if (!targets.Contains(tarEnemy))
                    {
                        targets.Add(tarEnemy);
                        GameObject Fx = SkillPoolManager.Release(data.Pools[0].prefab, target.transform.position, Quaternion.identity);
                        Fx.transform.parent = target.transform;
                        FxFlame.Add(Fx);
                        yield return new WaitForSeconds(0.05f);
                    }
                    yield return null;
                }

                yield return null;
            }
        }

        /// <summary>
        /// 引发爆炸
        /// </summary>
        /// <returns></returns>
        public IEnumerator FlameBomb()
        {
            if (FxFlame.Count != targets.Count) yield break;
            for (int i = 0; i < targets.Count; i++)
            {
                GameObject Fx = SkillPoolManager.Release(data.Pools[1].prefab, targets[i].transform.position, 
                    Quaternion.identity);
                WaitUtils.WaitTimeDo(data.Duration, () => Fx.gameObject.SetActive(false));
                FxFlame[i].gameObject.SetActive(false);
                GameManager.Instance.OptionDamage(Player,targets[i],data,targets[i].transform.position);
                yield return new WaitForSeconds(0.1f);
            }
        }


        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
    }

}
