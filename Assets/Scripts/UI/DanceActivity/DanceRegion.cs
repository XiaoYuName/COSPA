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
        private Button GameSceneBtn;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Back/Top/CloseBtn");
            Bind(CloseBtn,Close,UiAudioID.OnChick);
            MainBtn = Get<Button>("UIMask/Back/Down/ENTER");
            MainContent = Get("UIMask/Back/Center/MainContent");
            AudioContent = Get("UIMask/Back/Center/AudioContent");
            DanceConfig = ConfigManager.LoadConfig<DanceConfig>("Activity/DanceConfig");
            AudioContentUI = Get<SwitchAudioContentUI>("UIMask/Back/Center/AudioContent/Center");
            AudioContentUI.Init();
            GameSceneBtn = Get<Button>("UIMask/Back/Center/AudioContent/Down/Top_Back");
            InitSwitchAudioContentUI();
            Bind(MainBtn,()=>SwitchTbaleBtn(DancePanelType.AudioContent),UiAudioID.OnChick);
            Bind(GameSceneBtn,OnGameDenceScene,UiAudioID.OnChick);
        }

        private void InitSwitchAudioContentUI()
        {
            AudioContentUI.InitData(DanceConfig._danceDatas);
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
                    AudioContentUI.SetCurrentIndexUI();
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
    }
}

