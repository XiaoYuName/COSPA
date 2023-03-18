using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;

public class HomeScnen : UIBase
{
    private Button TaskBtn;
    private Button UpGameBtn;
    private Button ShorBtn;
    private Button DanceBtn;
    public override void Init()
    {
        TaskBtn = Get<Button>("UIMask/DownUI/Content/TaskBtn");
        UpGameBtn = Get<Button>("UIMask/DownUI/Content/UpGameBtn");
        Bind(TaskBtn, () =>
        {
            FadeManager.Instance.PlayFade(0.25f, delegate
            {
                UISystem.Instance.OpenUI("SystemTaskPanel");
                MainPanel.Instance.AddTbaleChild("SystemTaskPanel");
            }, 0.25f);
        },"OnChick");
        Bind(UpGameBtn,()=>UISystem.Instance.ShowPopNotice(),"OnChick");

        ShorBtn = Get<Button>("UIMask/DownUI/Content/ShortBtn");
        Bind(ShorBtn, () =>
        {
            FadeManager.Instance.PlayFade(0.25f, delegate
            {
                UISystem.Instance.OpenUI("EquipStoenPanel");
                MainPanel.Instance.AddTbaleChild("EquipStoenPanel");
            }, 0.25f);
        },UiAudioID.UI_click);
        WaitUtils.WaitTimeDo(1.5f, () => UISystem.Instance.ShowPopNotice());

        DanceBtn = Get<Button>("UIMask/TopUI/DanceBtn");
        Bind(DanceBtn,()=>UISystem.Instance.OpenUI("DanceActivity"),UiAudioID.OnChick);
    }
}
