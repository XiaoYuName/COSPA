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
        }

        private List<MagicLockFx> _lockFxes = new List<MagicLockFx>();
        private List<Enemy> Enemys = new List<Enemy>();
        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }

            if (EventName != "OpenMagicLock" && EventName != "BommMagicLock") return;

            if (EventName == "OpenMagicLock")
            {
                SkillPoolManager.Instance.StartCoroutine(PlayFx());
            }

            if (EventName == "BommMagicLock")
            {
                for (int i = 0; i < _lockFxes.Count; i++)
                {
                    _lockFxes[i].Boom();
                    GameManager.Instance.OptionDamage(Player,
                        Enemys[i],data,Enemys[i].GetPoint());
                }
            }

        }

        public IEnumerator PlayFx()
        {
            Collider2D[] targets = new Collider2D[(int)data.Duration];
            _lockFxes.Clear();
            Enemys.Clear();
            var size = Physics2D.OverlapCircleNonAlloc(Player.transform.position, data.Radius, targets, data.Mask);
            for (int i = 0; i < size; i++)
            {
                Enemy enemy = targets[i].gameObject.GetComponentInParent<Enemy>();
                if(enemy == null)continue;
                MagicLockFx lockFx = SkillPoolManager.Release(data.Pools[0].prefab, enemy.transform.position, Quaternion.identity)
                    .GetComponent<MagicLockFx>();
                Enemys.Add(enemy);
                _lockFxes.Add(lockFx);
                yield return new WaitForSeconds(data.ReleaseTime);

            }
        }
    }
}

