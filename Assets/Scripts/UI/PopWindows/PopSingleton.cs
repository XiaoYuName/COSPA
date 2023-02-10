using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

        public override void Init()
        {
            titleText = Get<TextMeshProUGUI>("UIMask/Back/Farme/Top/title");
            desText = Get<TextMeshProUGUI>("UIMask/Back/Farme/Info/des");
            QuitText = Get<TextMeshProUGUI>("UIMask/Back/Button/BtnText");
            QuitBtn = Get<Button>("UIMask/Back/Button");
            transform.localScale = Vector3.zero;
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
            transform.DOScale(Vector3.one, Settings.PopTweenTime);
            AudioManager.Instance.PlayAudio("PopWindows");
            titleText.text = title;
            desText.text = des;
            QuitText.text = quitText;
            Bind(QuitBtn, delegate
            {
                transform.DOScale(Vector3.zero, Settings.PopTweenTime).OnComplete(() =>
                {
                    UIMaskManager.Instance.SetMainScnenMask(false);
                    Close();
                });
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
            transform.DOScale(Vector3.one, Settings.PopTweenTime);
            AudioManager.Instance.PlayAudio("PopWindows");
            titleText.text = title;
            desText.text = des;
            QuitText.text = quitText;
            Bind(QuitBtn, delegate
            {
                transform.DOScale(Vector3.zero, Settings.PopTweenTime).OnComplete(() =>
                {
                    func?.Invoke();
                    UIMaskManager.Instance.SetMainScnenMask(false);
                    Close();
                });
            }, "OutChick");
          
        }
    }
}

