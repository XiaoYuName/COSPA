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
        public MoneyUI MoneyUI;
        public override void Init()
        {
            XunLianBtn = Get<Button>("UIMask/Right/ChileBtn_XunLian");
            OpenSwicthMapPanel = Get<Button>("UIMask/Right/MainPrincLine");
            MoneyUI = Get<MoneyUI>("UIMask/Right/UserInfo/MoneyPoint/MoneyUI");
            MoneyUI.Init();

            Bind(OpenSwicthMapPanel, delegate
            {
                FadeManager.Instance.PlayFade(2, delegate
                {
                    UISystem.Instance.OpenUI("RegionPanel");
                    MainPanel.Instance.AddTbaleChild("RegionPanel");
                }, 1.5f);
            }, "UI_click");
        }
    }
}

