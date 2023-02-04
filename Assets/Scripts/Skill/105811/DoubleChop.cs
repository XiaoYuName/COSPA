using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using ARPG;
using ARPG.Config;
using ARPG.Pool.Skill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    /// <summary>
    /// Skill 技能重斩
    /// </summary>
    public class DoubleChop : PlayerSkill
    {
        public override void Play()
        {
            //1.首先判断技能是否在CD 并且该技能没有在播放中
            if (isCold || Player.animSpeed == 0) return;
            //2.释放技能
            Player.anim.SetTrigger("Skill_1");
            base.Play();
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
    }
}

