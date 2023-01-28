using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 探索界面UI
    /// </summary>
    public class SpecialPanel : UIBase
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
            MainPanel.Instance.RemoveTableChild("SpecialPanel");
        }
    }
}

