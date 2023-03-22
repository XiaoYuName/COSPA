using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
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
        private DanceConfig DanceConfig;
        private SwitchAudioContentUI AudioContentUI;
        private DanceCharacterContentUI DanceCharacterContentUI;
        private GameObject CharacterPanel;
        private Button GameSceneBtn;
        private Button CharacterBtn;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Back/Top/CloseBtn");
            Bind(CloseBtn,Close,UiAudioID.OnChick);
            MainBtn = Get<Button>("UIMask/Back/Down/ENTER");
            MainContent = Get("UIMask/Back/Center/MainContent");
            AudioContent = Get("UIMask/Back/Center/AudioContent");
            DanceConfig = ConfigManager.LoadConfig<DanceConfig>("Activity/DanceConfig");
            AudioContentUI = Get<SwitchAudioContentUI>("UIMask/Back/Center/AudioContent/Center");
            DanceCharacterContentUI = Get<DanceCharacterContentUI>("UIMask/Back/Center/Character_Content");
            DanceCharacterContentUI.Init();
            AudioContentUI.Init();
            CharacterBtn = Get<Button>("UIMask/Back/Down/SwitchCharacterPanel");
            GameSceneBtn = Get<Button>("UIMask/Back/Center/AudioContent/Down/Top_Back");
            CharacterPanel = Get("UIMask/Back/Center/Character_Content");
            InitContentUI();
            Bind(CharacterBtn,()=>SwitchTbaleBtn(DancePanelType.CharacterContent),UiAudioID.OnChick);
            Bind(MainBtn,()=>SwitchTbaleBtn(DancePanelType.AudioContent),UiAudioID.OnChick);
            Bind(GameSceneBtn,OnGameDenceScene,UiAudioID.OnChick);
        }

        private void InitContentUI()
        {
            AudioContentUI.InitData(DanceConfig._danceDatas);
            DanceCharacterContentUI.InitData(DanceConfig._danceCharacter);
        }
        
        private void SwitchTbaleBtn(DancePanelType table)
        {
            switch (table)
            {
                case DancePanelType.MainCharacter:
                    MainContent.gameObject.SetActive(true);
                    AudioContent.gameObject.SetActive(false);
                    CharacterPanel.gameObject.SetActive(false);
                    Bind(CloseBtn,Close,UiAudioID.OnChick);
                    break;
                case DancePanelType.AudioContent:
                    MainContent.gameObject.SetActive(false);
                    AudioContent.gameObject.SetActive(true);
                    CharacterPanel.gameObject.SetActive(false);
                    AudioContentUI.SetCurrentIndexUI();
                    Bind(CloseBtn,()=>SwitchTbaleBtn(DancePanelType.MainCharacter),UiAudioID.OnChick);
                    break;
                case DancePanelType.CharacterContent:
                    MainContent.gameObject.SetActive(false);
                    AudioContent.gameObject.SetActive(false);
                    CharacterPanel.gameObject.SetActive(true);
                    Bind(CloseBtn,()=>SwitchTbaleBtn(DancePanelType.MainCharacter),UiAudioID.OnChick);
                    break;
            }
        }

        private void OnGameDenceScene()
        {
            DanceData SelectDance = AudioContentUI.GetCurrentDanceData();
            MainPanel.Instance.Close();
            MessageAction.OnTransitionEvent("DanceScene",Vector3.zero);
            Close();
            UISystem.Instance.CloseUI("DanceActivity");
            ActivityGameScene gameScene = UISystem.Instance.GetUI<ActivityGameScene>("DanceGameScene",true);
            gameScene.InitData(SelectDance);
        }
    }

    public enum DancePanelType
    {
        MainCharacter,
        AudioContent,
        CharacterContent,
    }
}

