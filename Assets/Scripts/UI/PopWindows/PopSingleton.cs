using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 单项提示框
    /// </summary>
    public class PopSingleton : UIBase
    {
        private TextMeshProUGUI titleText;
        private TextMeshProUGUI desText;
        private TextMeshProUGUI QuitText;
        private Button QuitBtn;
        private Animator anim;
        private static readonly int s_Show = Animator.StringToHash("Show");

        public override void Init()
        {
            titleText = Get<TextMeshProUGUI>("UIMask/Back/Top/title");
            desText = Get<TextMeshProUGUI>("UIMask/Back/Info/des");
            QuitText = Get<TextMeshProUGUI>("UIMask/Back/Button/BtnText");
            QuitBtn = Get<Button>("UIMask/Back/Button");
            anim = GetComponent<Animator>();
        }


        /// <summary>
        /// 显示提示弹窗
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="des">内容</param>
        /// <param name="quitText">确定按钮</param>
        public void ShowPopWindows(string title, string des, string quitText)
        {
            Open();
            AudioManager.Instance.PlayAudio("PopWindows");
            titleText.text = title;
            desText.text = des;
            QuitText.text = quitText;
            anim.SetTrigger(s_Show);
            Bind(QuitBtn, delegate
            {
                UIMaskManager.Instance.SetMainScnenMask(false);
                Close();
            },"OutChick");
            
        }
        
        /// <summary>
        /// 显示提示弹窗
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="des">内容</param>
        /// <param name="quitText">确定按钮</param>
        /// <param name="func">确定按钮点击后回调</param>
        public void ShowPopWindows(string title, string des, string quitText,Action func)
        {
            Open();
            AudioManager.Instance.PlayAudio("PopWindows");
            titleText.text = title;
            desText.text = des;
            QuitText.text = quitText;
            anim.SetTrigger(s_Show);
            Bind(QuitBtn, delegate
            {
                func?.Invoke();
                UIMaskManager.Instance.SetMainScnenMask(false);
                Close();
            }, "OutChick");
          
        }
    }
}

