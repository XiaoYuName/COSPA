using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 魔法师远程攻击
    /// </summary>
    public class MagicAttack : Skill
    {
        public override void Init(Character character, SkillType type, SkillItem item)
        {
            base.Init(character, type, item);
            MessageManager.Instance.Register<string>(C2C.EventMsg,AnimatorMsg);
        }

        public override void Play()
        {
        }

        public void AnimatorMsg(string EventName)
        {
            if (EventName != "MagicAttack")return;
            
            SkillPoolManager.Instance.StartCoroutine(StarMagicAttack());
        }

        public IEnumerator StarMagicAttack()
        {
           IDamage target = CheckTarget();
           if(target == null) yield break;
           Transform PlayerBonePos = Player.GetPoint("weaponMain_away");
           GameObject Bull =  SkillPoolManager.Release(data.Pools[0].prefab, PlayerBonePos.transform.position, Quaternion.identity);
           AudioManager.Instance.PlayAudio("MagicAttack");
           ParticleSystem StarFx = SkillPoolManager.Release(data.Pools[1].prefab, PlayerBonePos.transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
           SkillPoolManager.Instance.StartCoroutine(MovenemtToTargetDamager(Bull, target));
           yield return new WaitForSeconds(StarFx.main.duration);
           StarFx.gameObject.SetActive(false);
        }
        
        private IEnumerator MovenemtToTargetDamager(GameObject Bull,IDamage target)
        {
            Transform targetPos = target.GetPoint("body");
            while (isNotChekc(targetPos.gameObject)&&Vector2.Distance(Bull.transform.position,targetPos.position) > 0.15f)
            {
                Bull.transform.position = Vector3.MoveTowards(Bull.transform.position, targetPos.transform.position, 
                    data.ReleaseTime * Time.deltaTime);
                Vector3 dir = targetPos.position - Bull.transform.position;
                float angle = Vector3.SignedAngle(Vector3.right, dir, Vector3.forward);
                Bull.transform.eulerAngles = new Vector3(0, 0, angle);
                yield return null;
            }
            Bull.gameObject.SetActive(false);
            GameManager.Instance.OptionDamage(Player,target,data,targetPos.position);
            ParticleSystem HitFx = SkillPoolManager.Release(data.Pools[2].prefab, targetPos.position,Quaternion.Euler(-90,0,0)).GetComponent<ParticleSystem>();
            yield return new WaitForSeconds(HitFx.main.duration);
            HitFx.gameObject.SetActive(false);
        }


        /// <summary>
        /// 监测范围敌人
        /// </summary>
        /// <returns></returns>
        public IDamage CheckTarget()
        {
            Collider2D other = Physics2D.OverlapCircle(Player.GetPoint(), data.Radius, data.Mask);
            if (other != null && other.CompareTag("Character"))
            {
                return other.GetComponentInParent<Enemy>();
            }

            return null;
        }

        public bool isNotChekc(GameObject target)
        {
            if (Player != null && Player.gameObject.activeSelf && target != null && target.gameObject.activeSelf)
            {
                return true;
            }

            return false;
        }



        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AnimatorMsg);
        }
    }   
}

