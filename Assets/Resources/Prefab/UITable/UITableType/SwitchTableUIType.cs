using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SwitchTableUIType : UIBase
    {
        public EquipTableType tableType;
        private Image TableImage;
        public override void Init()
        {
            TableImage = GetComponent<Image>();
        }


        public void OnClick(bool isShowColor)
        {
            TableImage.color = isShowColor ? Color.white : new Color(1, 1, 1, 0);
        }
    }
}

