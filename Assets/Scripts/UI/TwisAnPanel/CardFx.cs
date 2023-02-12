using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 扭蛋显示奖励界面
    /// </summary>
    public class CardFx : UIBase
    {
        /// <summary>
        /// 星级Icon 图片
        /// </summary>
        private Image iconImage;

        private GameObject _FxSystem;
        private GameObject _FxBoom;

        private int CurrentStar;
        public override void Init()
        {
            iconImage = Get<Image>("Image (1)");
            _FxSystem = Get("Image (1)/PS");
            _FxBoom = Get("yi");
        }

        public void IniData(int Star)
        {
            CurrentStar = Star;
            string iconID = Star switch
            {
                1 => "Twist_Crad_OneTween",
                2 => "Twist_Crad_TwoTween",
                3 => "Twist_Crad_ThreeTween",
                _ => "Twist_Crad_OneTween",
            };
            iconImage.sprite = GameSystem.Instance.GetSprite(iconID);
            _FxSystem.gameObject.SetActive(Star == 3);
            _FxBoom.gameObject.SetActive(Star == 2);
        }


        public void OnPlayAudio()
        {
            string AudioID = CurrentStar switch
            {
                1 => "Twist_One",
                2 => "Twist_One",
                3 => "Twist_Three",
                _ => "Twist_One",
            };
            AudioManager.Instance.PlayAudio(AudioID);
        }

        public void SwitchSprite()
        {
            string iconID = CurrentStar switch
            {
                1 => "Twist_Crad_One",
                2 => "Twist_Crad_Two",
                3 => "Twist_Crad_Three",
                _ => "Twist_Crad_One",
            };
            iconImage.sprite = GameSystem.Instance.GetSprite(iconID);
        }
    }
}

