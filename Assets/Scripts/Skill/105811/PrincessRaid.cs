using System.Collections;
using System.Globalization;
using ARPG.BasePool;
using ARPG.Config;
using ARPG.Pool.Skill;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ARPG
{
    /// <summary>
    /// 公主突袭
    /// </summary>
    public class PrincessRaid : Skill
    {
        private Image Mask;
        private TextMeshProUGUI CdText;
        private bool isCold;
        public override void Init(Character character,SkillType type, SkillItem item)
        {
            base.Init(character,type, item);
            Mask = Player.attackButton.GetSkillCD(SkillType.Skill_03, out CdText);
            isCold = false;
            MessageManager.Instance.Register<string>(C2C.EventMsg,AniamtorMsg);
        }

        public void AniamtorMsg(string EventName)
        {
            if (Player == null)
            {
                Player = GameManager.Instance.Player;
            }
            switch (EventName)
            {
                case "StopVideoSkill3":
                    Player.StartCoroutine(WaitVideo());
                    break;
                case "StopSkill_3":
                    break;
                case "CreateSword":
                    PlaySkillFx();
                    break;
            }
        }

        public override void Play()
        {
            //1.首先判断技能是否在CD 并且该技能没有在播放中
            if (isCold || Player.animSpeed == 0) return;
            Player.StartCoroutine(WaitSkillTime(data.CD));
            //3.释放技能特效
            Player.anim.SetTrigger("Skill_3");
        }
        
        public IEnumerator WaitVideo()
        {
            VideoManager.Instance.PlayerAvVideo(data.VideoAsset);
            Player.anim.SetFloat("GlobalSpeed",0);
            yield return new WaitForSecondsRealtime(1.9f);
            Player.anim.SetFloat("GlobalSpeed",1);
        }

        public void PlaySkillFx()
        {
            float rotationY = Player.transform.rotation.eulerAngles.y > 0 ? -90:90;
            Vector3 PalyPoint = new Vector3(Player.body.position.x + data.RadiusOffset.x,
                Player.body.position.y + data.RadiusOffset.y, 0);
            SkillPoolManager.Release(data.Pools[0].prefab, PalyPoint, Quaternion.Euler(0,rotationY,-90));

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

        /// <summary>
        /// 计算技能了冷却CD
        /// </summary>
        /// <returns></returns>
        public IEnumerator WaitSkillTime(float skillCd)
        {
            isCold = true;
            Mask.fillAmount = 1;
            CdText.gameObject.SetActive(true);
            float skill = skillCd;
            while (skill >= 0)
            {
                skill -= Time.deltaTime;
                float fillAmount = Mathf.Min(((1 / skillCd)*Time.deltaTime),0.1f);
                Mask.fillAmount -= Mathf.Max(fillAmount, 0);
                string cd = Mathf.Ceil(skill).ToString(CultureInfo.InvariantCulture);
                CdText.text = cd;
                yield return null;
            }
            Mask.fillAmount = 0;
            CdText.gameObject.SetActive(false);
            isCold = false;
        }


        public override void UHandle()
        {
            base.UHandle();
            MessageManager.Instance.URegister<string>(C2C.EventMsg,AniamtorMsg);
        }
    }
}

