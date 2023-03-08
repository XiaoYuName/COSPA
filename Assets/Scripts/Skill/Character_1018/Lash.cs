using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;

namespace ARPG
{
    /// <summary>
    /// 鞭打
    /// </summary>
    public class Lash : PlayerSkill
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

        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }
            if (EventName != " Lash") return;
            PlayerFx();
        }

        public void PlayerFx()
        {
            Transform bodyTran = Player.GetPoint("legF");
            Collider2D other =  Physics2D.OverlapCircle(bodyTran.position, data.Radius,data.Mask);
            if (other == null || !other.CompareTag("Character")) return;
            IDamage target = other.transform.GetComponentInParent<Enemy>();
            Vector3 boundPoint = other.bounds.ClosestPoint(bodyTran.position);
            GameManager.Instance.OptionDamage(Player,target,data,boundPoint);
            SkillPoolManager.Release(data.Pools[1].prefab, boundPoint,Quaternion.identity);
        }



        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
    }
}

