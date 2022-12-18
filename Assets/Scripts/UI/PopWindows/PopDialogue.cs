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
        private Animator anim;
        private static readonly int s_Show = Animator.StringToHash("Show");

        public override void Init()
        {
            titleText = Get<TextMeshProUGUI>("UIMask/Back/Top/title");
            desText = Get<TextMeshProUGUI>("UIMask/Back/Info/des");
            QuitText = Get<TextMeshProUGUI>("UIMask/Back/CloseBtn/BtnText");
            QuitBtn = Get<Button>("UIMask/Back/CloseBtn");
            FuncBtn = Get<Button>("UIMask/Back/FuncBtn");
            FuncText = Get<TextMeshProUGUI>("UIMask/Back/FuncBtn/BtnText");
            anim = GetComponent<Animator>();
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
            anim.SetTrigger(s_Show);
            Bind(QuitBtn, delegate
            {
                quitFunc?.Invoke();
                Close();
            }, "UI_click");
            Bind(FuncBtn, delegate
            {
                func?.Invoke();
                Close();
            },"UI_click");
        }
    }
}

