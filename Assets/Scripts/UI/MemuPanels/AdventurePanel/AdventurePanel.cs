using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 冒险主界面
    /// </summary>
    public class AdventurePanel : UIBase
    {
        /// <summary>
        /// 训练场Button
        /// </summary>
        private Button XunLianBtn;
        private Button OpenSwicthMapPanel;
        private Button SpecialBtn; //探索Btn;
        public override void Init()
        {
            XunLianBtn = Get<Button>("UIMask/Right/ChileBtn_XunLian");
            OpenSwicthMapPanel = Get<Button>("UIMask/Right/MainPrincLine");
            SpecialBtn = Get<Button>("UIMask/Right/SpecialPanel");
            Bind(OpenSwicthMapPanel, delegate
            {
                FadeManager.Instance.PlayFade(0.25f, delegate
                {
                    UISystem.Instance.OpenUI("RegionPanel");
                    MainPanel.Instance.AddTbaleChild("RegionPanel");
                }, 0.25f);
            }, "UI_click");
            Bind(SpecialBtn, delegate
            {
                FadeManager.Instance.PlayFade(0.25f, delegate
                {
                    UISystem.Instance.OpenUI("SpecialPanel");
                    MainPanel.Instance.AddTbaleChild("SpecialPanel");
                }, 0.25f);
            }, "UI_click");
            
        }
    }
}

