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
        public Button XunLianBtn;
        public Button OpenSwicthMapPanel;
        public override void Init()
        {
            XunLianBtn = Get<Button>("UIMask/Right/ChileBtn_XunLian");
            OpenSwicthMapPanel = Get<Button>("UIMask/Right/MainPrincLine");

            Bind(OpenSwicthMapPanel, delegate
            {
                FadeManager.Instance.PlayFade(0.25f, delegate
                {
                    UISystem.Instance.OpenUI("RegionPanel");
                    MainPanel.Instance.AddTbaleChild("RegionPanel");
                }, 0.25f);
            }, "UI_click");
        }
    }
}

