using System.Collections;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    public abstract class Skill
    {
        protected Character Player;
        protected SkillItem data;
        protected Enemy Enemy;

        public virtual void Init(Character character, SkillItem item)
        {
            Player = character;
            data = item;
        }

        public virtual void Init(Enemy enemy, SkillItem item)
        {
            Enemy = enemy;
            data = item;
        }

        public abstract void Play();
    }

    /// <summary>
    /// 战士职业普通攻击
    /// </summary>
    public class SkillSoldierAttack : Skill
    {
        public override void Play()
        {
            Player.StartCoroutine(PlayFx());
        }

        private IEnumerator PlayFx()
        {
            yield return new WaitForSeconds(data.ReleaseTime);
            float rotationY = Player.transform.rotation.eulerAngles.y > 0 ? -90:90; 
            _FxItem fxItem = SkillPoolManager.Release(data.Pools[0].prefab, Player.body.position,
                Quaternion.Euler(0,rotationY,-90)).GetComponent<_FxItem>();
            fxItem.Play(Player,data);
        }
    }
    
    /// <summary>
    /// 魔法师职业普通攻击
    /// </summary>
    public class SkillConjurerAttack : Skill
    {
        public override void Play()
        {
            Player.StartCoroutine(PlayFx());
        }

        private IEnumerator PlayFx()
        {
            yield return new WaitForSeconds(data.ReleaseTime);
            Collider2D target = Physics2D.OverlapCircle(Player.body.position, data.Radius, data.Mask);
            if (target != null && target.CompareTag("Character"))
            {
                Vector3 Point = target.bounds.ClosestPoint(Player.body.position);
                GameObject Fx = SkillPoolManager.Release(data.Pools[0].prefab, Point, Quaternion.identity);
                GameManager.Instance.OptionDamage(Player,target.GetComponent<Enemy>(),data,Point);
                yield return new WaitForSeconds(data.Duration);
                Fx.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 怪物普通攻击
    /// </summary>
    public class SkillEnemySoldierAttack : Skill
    {
        private Transform AttackPoint;
        public override void Play()
        {
            Enemy.StartCoroutine(PlayFx());
        }
        private IEnumerator PlayFx()
        {
            AttackPoint = Enemy.transform.Find("AttackPoint");
            yield return new WaitForSeconds(data.ReleaseTime);
            _FxItem fxItem = SkillPoolManager.Release(data.Pools[0].prefab, AttackPoint.position,
                Quaternion.identity).GetComponent<_FxItem>();
            fxItem.Play(Enemy,data);
        }
        
    }
}

