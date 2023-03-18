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
        public override void Init()
        {
            CloseBtn = Get<Button>("Back/Close");
            Bind(CloseBtn,Close,UiAudioID.UI_click);
        }
    } 
}

