using System.Collections;
using System.Collections.Generic;
using ARPG.GameSave;
using ARPG.UI;
using DG.Tweening;
using TMPro;
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
        private GameSettingsUI[] settingsUis;
        private Button FPSBtn;
        private int[] FPSValues = new[] { 30, 60, 90, 120, 240 };
        private int index;
        private TextMeshProUGUI FSPValue;
        public override void Init()
        {
            BackTween = Get("UIMask/Back");
            isTween = false;
            index = 4;
            CloseBtn = Get<Button>("UIMask/Back/CloseBtn");
            SaveBtn = Get<Button>("UIMask/Back/Content/SaveBtn");
            settingsUis = GetComponentsInChildren<GameSettingsUI>();
            for (int i = 0; i < settingsUis.Length; i++)
            {
                settingsUis[i].Init();
            }
            Bind(CloseBtn,Close,UiAudioID.OutChick);
            Bind(SaveBtn,()=>InventoryManager.Instance.SaveUserData(),"UI_click");
            FPSBtn = Get<Button>("UIMask/Back/Content/FPSBtn");
            FSPValue = Get<TextMeshProUGUI>("UIMask/Back/Content/FPSBtn/icon/FPS");
            Bind(FPSBtn,OnSetFPS,UiAudioID.UI_click);
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


        private void OnSetFPS()
        {
            index++;
            if (index >= FPSValues.Length)
            {
                index = 0;
            }
            FSPValue.text = FPSValues[index].ToString();
            GameSystem.Instance.SetFPS(FPSValues[index]);
        }
    }

}
