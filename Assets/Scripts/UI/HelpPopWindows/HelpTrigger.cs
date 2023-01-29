using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class HelpTrigger : UIBase
    {
        private Button btn;
        public HelpType Type;

        public void Awake()
        {
            Init();
        }

        public override void Init()
        {
            btn = GetComponent<Button>();
            Bind(btn,OnClick,"OnChick");
        }


        private void OnClick()
        {
            HelpTextItem data = GameSystem.Instance.GetHelpItem(Type);
            UISystem.Instance.ShowPopWindows(data.title,data.description,"关闭",true);
        }
    }
}

