using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PopDialogue : UIBase
    {
        private TextMeshProUGUI titleText;
        private TextMeshProUGUI desText;
        private TextMeshProUGUI QuitText;
        private TextMeshProUGUI FuncText;
        private Button QuitBtn;
        private Button FuncBtn;

        public override void Init()
        {
            titleText = Get<TextMeshProUGUI>("UIMask/Back/Farme/Top/title");
            desText = Get<TextMeshProUGUI>("UIMask/Back/Farme/Info/des");
            QuitText = Get<TextMeshProUGUI>("UIMask/Back/CloseBtn/BtnText");
            QuitBtn = Get<Button>("UIMask/Back/CloseBtn");
            FuncBtn = Get<Button>("UIMask/Back/FuncBtn");
            FuncText = Get<TextMeshProUGUI>("UIMask/Back/FuncBtn/BtnText");
        }


        /// <summary>
        /// 显示对话弹窗
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="des">内容</param>
        /// <param name="quitText">Btn-1</param>
        /// <param name="funcText">Btn-2</param>
        /// <param name="quitFunc">Btn-1 点击后回调</param>
        /// <param name="func">Btn-2 点击后回调</param>
        public void ShowPopWindows(string title, string des, string quitText,string funcText,Action quitFunc,Action func)
        {
            Open();
            AudioManager.Instance.PlayAudio("PopWindows");
            titleText.text = title;
            desText.text = des;
            QuitText.text = quitText;
            FuncText.text = funcText;
            Bind(QuitBtn, delegate
            {
                quitFunc?.Invoke();
                UIMaskManager.Instance.SetMainScnenMask(false);
                Close();
            }, "UI_click");
            Bind(FuncBtn, delegate
            {
                func?.Invoke();
                UIMaskManager.Instance.SetMainScnenMask(false);
                Close();
            },"UI_click");
        }
    }
}

