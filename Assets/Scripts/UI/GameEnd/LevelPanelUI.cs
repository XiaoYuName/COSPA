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
        }


        public IEnumerator OpentionLevelAndFavorability(MapItem Reword)
        {
            
            yield return null;
        }
    }
}

