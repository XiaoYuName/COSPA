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

        List<GameObject> LookFxObj = new List<GameObject>();
        public IEnumerator PlaySkill()
        {
            Collider2D[] target = new Collider2D[5];
            Transform CharacterTran = Player.GetPoint("weaponMain_away").transform;
            int size = Physics2D.OverlapCircleNonAlloc(CharacterTran.position, data.Radius, target,
                data.Mask);
            if (size == 0) yield break;
            GameObject LoopFx = SkillPoolManager.Release(data.Pools[0].prefab, CharacterTran.transform.position,
                Quaternion.identity);
            List<Coroutine> LookFxList = new List<Coroutine>();
            LookFxObj.Clear();
            for (int i = 0; i < size; i++)
            {
                IDamage enemy = target[i].GetComponentInParent<IDamage>();
                Transform enemyBoneTran = enemy.GetPoint("body");
                while (Vector2.Distance(LoopFx.transform.position,enemyBoneTran.position) > 0.15f)
                {
                    LoopFx.transform.position = Vector2.MoveTowards(LoopFx.transform.position,
                        enemyBoneTran.position, data.ReleaseTime * Time.deltaTime);
                    yield return null;
                }
                Coroutine LookCoroutine = SkillPoolManager.Instance.StartCoroutine(LookTarget(enemy));
                LookFxList.Add(LookCoroutine);
            }
            LoopFx.gameObject.SetActive(false);
            for (int i = 0; i < LookFxList.Count; i++)
            {
                IDamage enemy = target[i].GetComponentInParent<IDamage>();
                Transform tagetTran = enemy.GetPoint("body");
                SkillPoolManager.Instance.StopCoroutine(LookFxList[i]);
                LookFxObj[i].gameObject.SetActive(false);
                SkillPoolManager.Release(data.Pools[1].prefab,tagetTran.position);
                GameManager.Instance.OptionDamage(Player,enemy,data,tagetTran.position);
                yield return new WaitForSeconds(0.15f);
            }
        }

        public IEnumerator LookTarget(IDamage target)
        {
            Transform enemyBoneTran = target.GetPoint("body");
            GameObject LoopFx = SkillPoolManager.Release(data.Pools[0].prefab, enemyBoneTran.position,
                Quaternion.identity);
            LookFxObj.Add(LoopFx);
            
            while (target.GetState().currentHp >= 0 && enemyBoneTran.gameObject.activeSelf)
            {
                LoopFx.transform.position = Vector2.MoveTowards(LoopFx.transform.position,
                    enemyBoneTran.position, data.ReleaseTime * Time.deltaTime);
                yield return null;
            }
        }

        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
    }
}

