using System;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class MemuTableType : UIBase
    {
        public MemuTableMode tableType;
        private Button OnBtn;
        private Image TableImage;
        

        public override void Init()
        {
            TableImage = GetComponent<Image>();
            OnBtn = GetComponent<Button>();
        }


        public void BindOnClick(Action<MemuTableMode> func)
        {
            Bind(OnBtn, () => { func?.Invoke(tableType); },UiAudioID.OnChick);
        }

        public void OnClick(bool isShowColor)
        {
            TableImage.color = isShowColor ? Color.white : new Color(1, 1, 1, 0);
        }
    } 
}

