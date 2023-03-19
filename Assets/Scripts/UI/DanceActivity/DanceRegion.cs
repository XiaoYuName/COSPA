using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    /// <summary>
    /// 喵斯快跑主界面
    /// </summary>
    public class DanceRegion : UIBase
    {
        private Button CloseBtn;
        private GameObject MainContent;
        private Button MainBtn;
        private GameObject AudioContent;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Back/Top/CloseBtn");
            Bind(CloseBtn,Close,UiAudioID.OnChick);
            MainBtn = Get<Button>("UIMask/Back/Down/ENTER");
            MainContent = Get("UIMask/Back/Center/MainContent");
            AudioContent = Get("UIMask/Back/Center/AudioContent");
            Bind(MainBtn,()=>SwitchTbaleBtn(DancePanelType.AudioContent),UiAudioID.OnChick);
        }

        private void SwitchTbaleBtn(DancePanelType table)
        {
            switch (table)
            {
                case DancePanelType.MainCharacter:
                    MainContent.gameObject.SetActive(true);
                    AudioContent.gameObject.SetActive(false);
                    Bind(CloseBtn,Close,UiAudioID.OnChick);
                    break;
                case DancePanelType.AudioContent:
                    MainContent.gameObject.SetActive(false);
                    AudioContent.gameObject.SetActive(true);
                    Bind(CloseBtn,()=>SwitchTbaleBtn(DancePanelType.MainCharacter),UiAudioID.OnChick);
                    break;
            }
        }
    }

    public enum DancePanelType
    {
        MainCharacter,
        AudioContent,
    }
}

