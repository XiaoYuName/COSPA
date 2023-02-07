using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;

public class HoemScnen : UIBase
{
    private Button TaskBtn;
    public override void Init()
    {
        TaskBtn = Get<Button>("UIMask/DownUI/Content/TaskBtn");
        Bind(TaskBtn, () =>
        {
            FadeManager.Instance.PlayFade(0.25f, delegate
            {
                UISystem.Instance.OpenUI("SystemTaskPanel");
                MainPanel.Instance.AddTbaleChild("SystemTaskPanel");
            }, 0.25f);
        },"OnChick");
    }
}
