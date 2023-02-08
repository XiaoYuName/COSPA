using System.Collections;
using System.Collections.Generic;
using ARPG.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    public class PopSettings : UIBase
    {
        private Button CloseBtn;
        private Button SaveBtn;
        private GameObject BackTween;
        private bool isTween;
        public override void Init()
        {
            BackTween = Get("UIMask/Back");
            isTween = false;
            CloseBtn = Get<Button>("UIMask/Back/CloseBtn");
            SaveBtn = Get<Button>("UIMask/Back/Content/SaveBtn");
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
