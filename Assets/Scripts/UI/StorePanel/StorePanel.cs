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
        private Button GemsthoneBtn;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OutChick");
            GemsthoneBtn = Get<Button>("UIMask/SwitchBtns/GemsthoneBtn");
            Bind(GemsthoneBtn,()=>UISystem.Instance.OpenUI("StorePopWindows"),"OnChick");
        }
    }
}

