using System.Collections;
using System.Collections.Generic;
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
        public override void Init()
        {
            QuitBtn = GetComponent<Button>();
            Bind(QuitBtn,()=>UISystem.Instance.OpenUI("MemuPanel"),"OnChick");
        }
    }
}

