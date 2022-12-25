using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PlotPanel : UIBase
    {
        public Button OpenSwicthMapPanel;
        public MoneyUI MoneyUI;
        public override void Init()
        {
            OpenSwicthMapPanel = Get<Button>("UIMask/Right/MianPlot");
            MoneyUI = Get<MoneyUI>("UIMask/Right/MoneyUI");
            MoneyUI.Init();
            Bind(OpenSwicthMapPanel, delegate
            {
                FadeManager.Instance.PlayFade(2, delegate
                {
                    UISystem.Instance.OpenUI("SwicthMapPanel");
                    MainPanel.Instance.AddTbaleChild("SwicthMapPanel");
                }, 1.5f);
            }, "UI_click");
        }
    }
}

