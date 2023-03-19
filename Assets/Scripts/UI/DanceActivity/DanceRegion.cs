using System.Collections;
using System.Collections.Generic;
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
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Back/Top/CloseBtn");
            Bind(CloseBtn,Close,UiAudioID.OnChick);
        }
    }
}

