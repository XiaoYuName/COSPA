using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ARPG
{
    /// <summary>
    /// 公用Player Skill 子类
    /// </summary>
    public class PlayerSkill : Skill
    {
        protected Image Mask;
        protected TextMeshProUGUI CdText;
        protected bool isCold;
        public override void Init(Character character,SkillType type, SkillItem item)
        {
            base.Init(character,type, item);
            Mask = Player.attackButton.GetSkillCD(type, out CdText);
        }

        public override void Play()
        {
            Player.StartCoroutine(WaitSkillTime(data.CD));
        }

        /// <summary>
        /// 计算技能了冷却CD
        /// </summary>
        /// <returns></returns>
        protected IEnumerator WaitSkillTime(float skillCd)
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

