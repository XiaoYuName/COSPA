using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 宝石商店面板
    /// </summary>
    public class StorePanel : UIBase
    {
        private Button CloseBtn;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"UI_click");
        }
    }
}

