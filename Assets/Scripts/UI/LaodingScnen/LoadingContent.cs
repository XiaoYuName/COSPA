using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class LoadingContent : UIBase
    {
        private TextMeshProUGUI title;
        private Button GoButton;
        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            title = Get<TextMeshProUGUI>("Title");
            GoButton = GetComponent<Button>();
            Bind(GoButton, delegate
            {
                MessageAction.OnTransitionEvent("GameScnen",Vector3.zero);
            }, "UI_click");
            title.DOFade(1, 2.5f).SetLoops(0,LoopType.Yoyo);
        }
        
        
    }
}

