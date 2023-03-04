using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using ARPG.UI.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 独立副本UI
    /// </summary>
    public class RegionSingUI : UIBase
    {
        private Button RegSingBtn;
        public SingRegionType RegionType;

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            RegSingBtn = GetComponent<Button>();
            Bind(RegSingBtn,OnClick,"OnChick");
        }

        private void OnClick()
        {
            MainPanel.Instance.AddTbaleChild("RegionToolTip");
            UISystem.Instance.OpenUI<RegionToolTip>("RegionToolTip",(ui)=>ui.InitData(ConfigSystem.Instance.GetSingRegion(RegionType.ToString())));
        }
        public enum SingRegionType
        {
            经验值关卡,
            玛那关卡,
            无尽星海,
            神圣殿堂
        }
    }

}

