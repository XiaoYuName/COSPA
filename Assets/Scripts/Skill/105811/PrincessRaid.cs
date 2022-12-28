using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ARPG;
using ARPG.BasePool;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ARPG
{
    public class PrincessRaid : Skill
    {
        private Image Mask;
        private TextMeshProUGUI CdText;
        private bool isCold;
        public override void Init(Character character, SkillItem item)
        {
            base.Init(character, item);
            Mask = Player.attackButton.GetSkillCD(3, out CdText);
            isCold = false;
        }
    
        public override void Play()
        {
            //1.首先判断技能是否在CD 并且该技能没有在播放中
            if (isCold || Player.animSpeed == 0) return;
            Player.StartCoroutine(WaitSkillTime(data.CD));
            //3.释放技能特效
            Player.StartCoroutine(PlayFx());
        }

        public IEnumerator PlayFx()
        {
           VideoManager.Instance.PlayerAvVideo(data.ID);
           Player.anim.SetTrigger("Skill_3");
           yield return new WaitForSeconds(0.25f);
           Time.timeScale = 0;
           yield return new WaitForSecondsRealtime(2f);
           Time.timeScale = 1;
          
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
    }
}

