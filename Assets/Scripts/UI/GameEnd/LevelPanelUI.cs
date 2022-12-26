using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class LevelPanelUI : UIBase
    {
        //等级
        private TextMeshProUGUI LevelText;
        /// <summary>
        /// 好感度
        /// </summary>
        private TextMeshProUGUI FavorabilityText;
        
        /// <summary>
        /// 经验滑动条
        /// </summary>
        private Slider LevelSlider;

        private TextMeshProUGUI LevelSliderText;

        /// <summary>
        /// 好感度滑动条
        /// </summary>
        private Slider FavorabilitySlider;
        
        public override void Init()
        {
            LevelText = Get<TextMeshProUGUI>("Level/Value");
            FavorabilityText = Get<TextMeshProUGUI>("Favorability/Value");
            LevelSlider = Get<Slider>("LevelSlider");
            FavorabilitySlider = Get<Slider>("FavorabilitySlider");
            LevelSliderText = Get<TextMeshProUGUI>("LevelSlider/SliderText");
        }


        /// <summary>
        /// 运算奖励道具
        /// </summary>
        /// <param name="Reword"></param>
        /// <returns></returns>
        public IEnumerator OpentionLevelAndFavorability(MapItem Reword)
        {
            CharacterBag characterBag = InventoryManager.Instance.GetBag(
                GameManager.Instance.Player.currentBag.ID);
            
            LevelText.text = characterBag.Level.ToString();
            FavorabilityText.text = characterBag.Favorability.ToString();
            
            LevelSlider.value = characterBag.exp;
            LevelSlider.minValue = 0;
            LevelSlider.maxValue = characterBag.MaxExp;
            for (int i = 0; i < Reword.MoneyReword.Length; i++)
            {
                if (Reword.MoneyReword[i].itemBag.ID != Settings.ExpID) continue;
                if (Reword.MoneyReword[i].itemBag.count > 0)
                {
                    int exp = Reword.MoneyReword[i].itemBag.count;
                    yield return AddUserExp(characterBag,exp);
                }
            }

            yield return null;
        }

        private IEnumerator AddUserExp(CharacterBag bag,int value)
        {
            //1.获取经验差值
            int lerp = bag.MaxExp - bag.exp;  // 30
            int temp = 0;
            LevelSlider.value = bag.exp;
            if (value >= lerp)
            {
                while (true)
                {
                    temp += 1;
                    if (temp >= LevelSlider.maxValue)
                    {
                        bag.Level++;
                        LevelSlider.maxValue = (Settings.deftualExp * bag.Level) * bag.currentStar;
                        LevelText.text = bag.Level.ToString();
                        LevelSlider.value = 0;
                        LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;
                        bag.MaxExp = (Settings.deftualExp * bag.Level) * bag.currentStar;
                        bag.exp = 0;
                        int sValue = value - lerp;
                        GameManager.Instance.Player.anim.SetTrigger("UpLevel");
                        yield return StartCoroutine(AddUserExp(bag, sValue));
                        yield break;
                    }
                    LevelSlider.value += temp;
                    LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;
                    yield return new WaitForSeconds(0.025f);
                }
            }
            while (temp < value)
            {
                temp += 1;
                LevelSlider.value += 1;
                bag.exp = temp;
                LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;
                yield return new WaitForSeconds(0.025f);
            }
            
        }
    }
}

