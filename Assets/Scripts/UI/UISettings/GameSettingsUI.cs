using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class GameSettingsUI : UIBase
    {
        public AudioMixerGroupType settingUIType;
        private Slider MainSlider;
        private TextMeshProUGUI SliderValue;
        public override void Init()
        {
            MainSlider = Get<Slider>("Slider_001");
            SliderValue = Get<TextMeshProUGUI>("Slider_001/SliderText");
            MainSlider.onValueChanged.AddListener(OnValueChanged);
            float value = AudioManager.Instance.GetMaskValue(settingUIType)*100;
            MainSlider.value = value;
            SliderValue.text = value + "/" + MainSlider.maxValue;
        }

        public void OnValueChanged(float value)
        {
            SliderValue.text = MainSlider.value + "/" + MainSlider.maxValue;
            AudioManager.Instance.SetAudioTypeVolme(settingUIType,value/100);
        }
    }
    
    
    
}

