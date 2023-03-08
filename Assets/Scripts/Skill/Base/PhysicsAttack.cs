using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 物理通用普攻
    /// </summary>
    public class PhysicsAttack : Skill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AnimatorMsg);
        }
        
        public void AnimatorMsg(string EventName)
        {
            if (EventName != "SkillSoldierAttack")return;
            float rotationY = Player.transform.rotation.eulerAngles.y > 0 ? -90:90; 
            SkillPoolManager.Release(data.Pools[0].prefab, Player.body.position, Quaternion.Euler(0,rotationY,-90));

            Collider2D[] targets = new Collider2D[10];
            int size = Physics2D.OverlapCircleNonAlloc(Player.body.position, data.Radius, targets,data.Mask);
            if (size <= 0) return;
            foreach (var enemy in targets)
            {
                if(enemy== null)continue;
                if (!enemy.gameObject.CompareTag("Character"))continue;
                IDamage target = enemy.GetComponentInParent<Enemy>();
                Vector3 boundPoint = enemy.bounds.ClosestPoint(Player.body.position);
                GameManager.Instance.OptionDamage(Player,target,data,boundPoint);
            }
        }
        
        
        public override void Play()
        {
            if(Player.animSpeed == 0)return;
            Player.anim.SetTrigger("Attack");
        }
        
        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AnimatorMsg);
        }
    }
}

