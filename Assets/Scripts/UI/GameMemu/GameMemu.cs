using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// Game场景下主菜单
    /// </summary>
    public class GameMemu : UIBase
    {
        private Button QuitBtn;
        private TextMeshProUGUI VaveText;
        public override void Init()
        {
            QuitBtn = Get<Button>("MemuBtn");
            VaveText = Get<TextMeshProUGUI>("Wave/Count");
            Bind(QuitBtn,()=>UISystem.Instance.OpenUI("MemuPanel"),"OnChick");
        }

        public void SetVaveText(string amount)
        {
            VaveText.text = amount;
        }
    }
}

