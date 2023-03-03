using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using RPG.Transition;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 主线显示UI
    /// </summary>
    public class RegionLineUI : UIBase
    {
        private RegionLine data;
        private Button OnClickBtn;
        private TextMeshProUGUI LineTitleName;
        private TextMeshProUGUI LineDesName;
        private TextMeshProUGUI LineRegionModeName;
        private Image LineRegionModeIcon;
        private GameObject ProgressContent;
        private LookState lookState;
        private Image BackIcon;

        public override void Init()
        {
            LineTitleName = Get<TextMeshProUGUI>("Down/RegionName");
            LineDesName = Get<TextMeshProUGUI>("Down/RegionTitle");
            LineRegionModeName = Get<TextMeshProUGUI>("Down/LineRegionModeIcon/LineRegionModeName");
            LineRegionModeIcon = Get<Image>("Down/LineRegionModeIcon");
            ProgressContent = Get("ProgressContent");
            OnClickBtn = GetComponent<Button>();
            BackIcon = GetComponent<Image>();
            Bind(OnClickBtn,LoadWordMapScene,UiAudioID.UI_Bc_Click);
        }

        public void InitData(RegionLine data)
        {
            this.data = data;
            LineTitleName.text = data.RegionName;
            LineDesName.text = data.RegionName;
            LineRegionModeName.text = data.Mode.ToString();
            LineRegionModeIcon.sprite = GameSystem.Instance.GetRegionFarem(data.Mode);
            BackIcon.sprite = data.backIcon;
            SetProgress(LookState.未开启);
        }

        public void SetProgress(LookState lookState)
        {
            this.lookState = lookState;
            switch (lookState)
            {
                case LookState.已通关:
                    ProgressContent.gameObject.SetActive(true);
                    ProgressContent.transform.GetChild(0).gameObject.SetActive(true);
                    ProgressContent.transform.GetChild(1).gameObject.SetActive(false);
                    break;
                case LookState.已解锁:
                    ProgressContent.gameObject.SetActive(false);
                    break;
                case LookState.未开启:
                    ProgressContent.gameObject.SetActive(true);
                    ProgressContent.transform.GetChild(0).gameObject.SetActive(false);
                    ProgressContent.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                default:
                    Debug.LogWarning("未知的解锁状态");
                    break;
            }
        }

        private void LoadWordMapScene()
        {
            if(data == null)return;
            if (data.SceneGrid == null)
            {
                UISystem.Instance.ShowTips("暂未开放");
                return;
            }
            if (lookState == LookState.未开启)
            {
                UISystem.Instance.ShowTips("需通关上一个关卡");
                return;
            }


            IEnumerator WaitLoad = LoadRegionUI(data);
            void EndAction()
            {
                RegionScene regionScene = UISystem.Instance.GetUI<RegionScene>("RegionScene");
                regionScene.PlayTweenBar();
            }
            FadeManager.Instance.PlayFade(1.15f,WaitLoad,null,null,EndAction);
        }

        private IEnumerator LoadRegionUI(RegionLine data)
        {
            RegionScene regionScene =  UISystem.Instance.GetUI<RegionScene>("RegionScene");
            MainPanel.Instance.AddTbaleChild("RegionScene");
            yield return regionScene.InitData(data);
        }
    } 
}

