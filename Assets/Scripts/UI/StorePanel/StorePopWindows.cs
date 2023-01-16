using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 宝石商店弹窗
    /// </summary>
    public class StorePopWindows : UIBase
    {
        private Button CloseBtn;
        private RectTransform content;

        public override void Init()
        {
            transform.localScale = Vector3.zero;
            CloseBtn = Get<Button>("UIMask/Back/Close");
            Bind(CloseBtn, Close, "OutChick");
            content = Get<RectTransform>("UIMask/Back/Scroll View/Viewport/Content");
        }


        public override void Close()
        {
            base.Close();
            transform.localScale  = Vector3.zero;
        }

        public override void Open()
        {
            base.Open();
            transform.DOScale(new Vector3(1, 1, 1), 0.25f).SetEase(Ease.Linear);
        }
    }
}

