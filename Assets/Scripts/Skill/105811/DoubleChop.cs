using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ARPG;
using ARPG.Pool.Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    /// <summary>
    /// Skill 技能重斩
    /// </summary>
    public class DoubleChop : Skill
    {
        private Image Mask;
        private TextMeshProUGUI CdText;
        private bool isCold;
        public override void Init(Character character, SkillItem item)
        {
            base.Init(character, item);
            Mask = Player.attackButton.GetSkillCD(1, out CdText);
            isCold = false;
        }

        public override void Play()
        {
            //1.首先判断技能是否在CD 并且该技能没有在播放中
            if (isCold || Player.animSpeed == 0) return;
            //2.释放技能
            Player.anim.SetTrigger("Skill_1");
            Player.StartCoroutine(WaitSkillTime(data.CD));
            //3.释放技能特效
            Player.StartCoroutine(PlayFx());
        }
        private IEnumerator PlayFx()
        {
            yield return new WaitForSeconds(data.ReleaseTime);
            float rotationY = Player.transform.rotation.eulerAngles.y > 0 ? 180:0; 
            _FxItem fxItem = SkillPoolManager.Release(data.Pools[0].prefab, Player.body.position,
                Quaternion.Euler(0,rotationY,0)).GetComponent<_FxItem>();
            fxItem.Play(Player,data);
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

