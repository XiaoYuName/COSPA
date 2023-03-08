using System;
using System.Collections;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    public abstract class Skill
    {
        protected Character Player;
        public SkillItem data;
        protected Enemy Enemy;
        protected SkillType _type;

        public virtual void Init(Character character, SkillType type,SkillItem item)
        {
            Player = character;
            data = item;
            _type = type;
        }

        public virtual void Init(Enemy enemy, SkillItem item)
        {
            Enemy = enemy;
            data = item;
        }

        public abstract void Play();

        public virtual void UHandle()
        {
        }
    }

    /// <summary>
    /// 战士职业普通攻击
    /// </summary>
    public class SkillSoldierAttack : Skill
    {
        public override void Play()
        {
            if(Player.animSpeed == 0)return;
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
           
        }

        private IEnumerator PlayFx()
        {
            yield return new WaitForSeconds(data.ReleaseTime);
            Collider2D target = Physics2D.OverlapCircle(Player.body.position, data.Radius, data.Mask);
            if (target != null && target.CompareTag("Character"))
            {
                Vector3 Point = target.bounds.ClosestPoint(Player.body.position);
                GameObject Fx = SkillPoolManager.Release(data.Pools[0].prefab, Point, Quaternion.identity);
                GameManager.Instance.OptionDamage(Player,target.transform.parent.GetComponent<Enemy>(),data,Point);
                yield return new WaitForSeconds(data.Duration);
                Fx.gameObject.SetActive(false);
            }
        }
    }

   

}

