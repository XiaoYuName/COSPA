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
        /// 经验滑动条
        /// </summary>
        private Slider LevelSlider;

        private TextMeshProUGUI LevelSliderText;

        public override void Init()
        {
            LevelText = Get<TextMeshProUGUI>("Level/Value");
            LevelSlider = Get<Slider>("LevelSlider");
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
        
        public IEnumerator OpentionLevelAndFavorability(RandomMapItem Reword)
        {
            CharacterBag characterBag = InventoryManager.Instance.GetBag(
                GameManager.Instance.Player.currentBag.ID);
            
            LevelText.text = characterBag.Level.ToString();

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
            //1.获取添加的经验总值
            int CurrentExp = value;
            //2.循环体： 当经验总值为0 退出
            while (CurrentExp >= 0)
            {
                //3.计算当前等级升满所需经验值
                int currentLevel = bag.MaxExp - bag.exp;
                //4.循环体进入累加经验值循环,直到经验值满足条件
                int temp = bag.exp;
                LevelSlider.value = bag.exp;
                float t= 0;
                if (CurrentExp < currentLevel) //当前所要添加的经验值不足以进行升级
                {
                    while (true)
                    {
                        if (temp >= bag.exp+CurrentExp)
                        {
                            bag.exp += CurrentExp;
                            LevelSlider.value = bag.exp;
                            LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;
                            yield break;
                        }
                        t += Time.deltaTime;
                        temp = (int)Mathf.LerpUnclamped(temp, bag.exp+CurrentExp, t);
                        LevelSlider.value = temp;
                        LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;
                        yield return null;
                    }
                }
                CurrentExp -= currentLevel;
                while (true)
                {
                    LevelSlider.value = temp;
                    LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;
                    if (temp >= currentLevel)
                    {
                        //进行升级操作
                        bag.Level++; //等级增加
                        LevelSlider.maxValue = (Settings.deftualExp * bag.Level) * bag.currentStar; //重新计算最大经验值
                        LevelText.text = bag.Level.ToString();//刷新Text显示
                        LevelSlider.value = 0;//升级后重置当前经验值
                        LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;////刷新Text显示
                        bag.MaxExp = (Settings.deftualExp * bag.Level) * bag.currentStar;//重新计算最大经验值
                        bag.exp = 0;
                        GameManager.Instance.Player.anim.SetTrigger("UpLevel");
                        break;
                    }
                    t += Time.deltaTime;
                    temp = (int)Mathf.LerpUnclamped(temp, currentLevel, t);
                    LevelSlider.value = temp;
                    LevelSliderText.text = LevelSlider.value + "/" + LevelSlider.maxValue;
                    yield return null;
                }
                
                yield return null;
            }
        }
    }
}

