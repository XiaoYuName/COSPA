using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    /// <summary>
    /// 喵斯快跑活动
    /// </summary>
    public class DanceActivity : UIBase
    {
        private Button CloseBtn;
        private Button DanceRgionBtn;
        public override void Init()
        {
            CloseBtn = Get<Button>("Back/Close");
            Bind(CloseBtn,Close,UiAudioID.UI_click);
            DanceRgionBtn = Get<Button>("Back/Right/ActivityRegion");
            Bind(DanceRgionBtn,()=>
                UISystem.Instance.OpenUI("DanceRegion"),UiAudioID.UI_click);
        }
    } 
}

