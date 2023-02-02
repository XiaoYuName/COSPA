using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PlotPanel : UIBase
    {
        public Button OpenTalePanel;
        public override void Init()
        {
            OpenTalePanel = Get<Button>("UIMask/Right/MianPlot");
            Bind(OpenTalePanel, delegate
            {
                FadeManager.Instance.PlayFade(0.25f, delegate
                {
                    MainPanel.Instance.AddTbaleChild("TalePanel");
                    UISystem.Instance.OpenUI("TalePanel");
                }, 0.25f);
            }, "OnChick");
        }
    }
}

