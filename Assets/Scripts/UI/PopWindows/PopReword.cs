using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PopReword : UIBase
    {
        private RectTransform content;
        private Button CloseBtn;
        private GameObject BackTween;
        private bool isTween;
        public override void Init()
        {
            content = Get<RectTransform>("UIMask/Back/RewordView/Viewport/Content");
            CloseBtn = Get<Button>("UIMask/Back/CloseBtn");
            Bind(CloseBtn,Close,"OutChick");
            BackTween = Get("UIMask/Back");
            isTween = false;
            BackTween.transform.localScale = Vector3.zero;
        }

        public void ShowReword(List<ItemBag> RewordList)
        {
            UIHelper.Clear(content);
            Open();
            foreach (var rBag in RewordList)
            {
                RewordLineUI lineUI =  UISystem.Instance.InstanceUI<RewordLineUI>("RewordLineUI", content);
                lineUI.InitData(rBag);
            }
        }
        
        public override void Open()
        {
            base.Open();
            isTween = true;
            BackTween.transform.DOScale(new Vector3(1, 1, 1), Settings.isShowItemTime).OnComplete(()=>isTween=false);
        }

        public override void Close()
        {
            if (isTween) return;
            BackTween.transform.DOScale(new Vector3(0, 0, 0), Settings.isShowItemTime).OnComplete(()=>base.Close());
        }
    }
}

