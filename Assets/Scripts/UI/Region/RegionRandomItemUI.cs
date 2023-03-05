using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


namespace ARPG
{
    [RequireComponent(typeof(Button))]
    public class RegionRandomItemUI : UIBase
    {
        private Button Button;

        public RegionRandomType Type;
        public Image Tweenicon;
        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            Button = GetComponent<Button>();
            Bind(Button, OnClick, UiAudioID.UI_Bc_Click);
            if (Tweenicon != null)
            {
                Tweenicon.transform.DOScale(new Vector3(1.15f, 1.15f, 0), 0.15f).SetLoops(-1, LoopType.Yoyo);
            }

        }

        private void OnClick()
        {
            MainPanel.Instance.AddTbaleChild("RegionToolTip");
            UISystem.Instance.OpenUI<RegionToolTip>("RegionToolTip",(ui)=>
                ui.InitData(ConfigSystem.Instance.GetSingRegion(Type.ToString())));
        }
    }
}

