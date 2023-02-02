using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 地下城界面
    /// </summary>
    public class DungeonsPanel : UIBase
    {
        private Button CloseBtn;
        
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OutChick");
            
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("DungeonsPanel");
        }
    }
}

