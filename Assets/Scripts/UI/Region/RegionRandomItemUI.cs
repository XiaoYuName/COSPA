using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using UnityEngine;
using UnityEngine.UI;


namespace ARPG
{
    [RequireComponent(typeof(Button))]
    public class RegionRandomItemUI : UIBase
    {
        private Button Button;

        public RegionRandomType Type;
        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            Button = GetComponent<Button>();
            Bind(Button, OnClick, UiAudioID.UI_Bc_Click);
        }

        private void OnClick()
        {
            MainPanel.Instance.AddTbaleChild("RegionToolTip");
            UISystem.Instance.OpenUI<RegionToolTip>("RegionToolTip",(ui)=>
                ui.InitData(ConfigSystem.Instance.GetSingRegion(Type.ToString())));
        }
    }
}

