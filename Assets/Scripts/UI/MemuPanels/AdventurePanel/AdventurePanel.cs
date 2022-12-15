using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class AdventurePanel : UIBase
    {
        /// <summary>
        /// 训练场Button
        /// </summary>
        public Button XunLianBtn;
        public override void Init()
        {
            XunLianBtn = Get<Button>("UIMask/Right/ChileBtn_XunLian");
            Bind(XunLianBtn, delegate
            {
                Close();
                MessageAction.OnTransitionEvent("MainScene",Vector3.zero);
            }, "UI_click");
        }
    }
}

