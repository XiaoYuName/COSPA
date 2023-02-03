using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PopTips : UIBase
    {
        private Image Tips;
        private TextMeshProUGUI description;
        public override void Init()
        {
            Tips = Get<Image>("UIMask/Tipls");
            description = Get<TextMeshProUGUI>("UIMask/Tipls/description");
        }
        
        
        /// <summary>
        /// 显示Tips提示框
        /// </summary>
        /// <param name="description">显示文本</param>
        /// <param name="waitTime">显示时长</param>
        /// <param name="time">动画过度时长</param>
        public void Show(string description,float waitTime,float time =0.25f)
        {
            Tips.fillAmount = 0;
            this.description.gameObject.SetActive(false);
            this.description.text = description;
            Tips.fillOrigin = (int)Image.OriginHorizontal.Left;
            Open();
            StartCoroutine(Tween(waitTime,time,null,null));
        }
        
        public void Show(string description,IEnumerator waitAction,Action endAction = null,float time =0.25f)
        {
            Tips.fillAmount = 0;
            this.description.gameObject.SetActive(false);
            this.description.text = description;
            Tips.fillOrigin = (int)Image.OriginHorizontal.Left;
            Open();
            StartCoroutine(Tween(time,waitAction,endAction));
        }
        
        public void Show(string description,float waitTime,Action endAction, float time =0.25f)
        {
            Tips.fillAmount = 0;
            this.description.gameObject.SetActive(false);
            this.description.text = description;
            Tips.fillOrigin = (int)Image.OriginHorizontal.Left;
            Open();
            StartCoroutine(Tween(waitTime,time,null,endAction));
        }
        
        public void Show(string description,float waitTime,Action endAction,Action waitAction, float time =0.25f)
        {
            Tips.fillAmount = 0;
            this.description.gameObject.SetActive(false);
            this.description.text = description;
            Tips.fillOrigin = (int)Image.OriginHorizontal.Left;
            Open();
            StartCoroutine(Tween(waitTime,time,waitAction,endAction));
        }
        private IEnumerator Tween(float waitTime, float time,Action waitAction,Action endAction)
        {
            yield return Tips.DOFillAmount(1,time);
            yield return new WaitForSeconds(time);
            
            description.gameObject.SetActive(true);
            waitAction?.Invoke();
            yield return new WaitForSeconds(waitTime);
            Tips.fillOrigin = (int)Image.OriginHorizontal.Right;
            
            
            description.gameObject.SetActive(false);
            yield return Tips.DOFillAmount(0,time);
            yield return new WaitForSeconds(time);
            endAction?.Invoke();
            Close();
            Tips.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
        
        private IEnumerator Tween(float time,IEnumerator waitAction,Action endAction)
        {
            yield return Tips.DOFillAmount(1,time).SetEase(Ease.Linear);
            yield return new WaitForSeconds(time);
            
            description.gameObject.SetActive(true);
            yield return waitAction;
            
            
            Tips.fillOrigin = (int)Image.OriginHorizontal.Right;
            description.gameObject.SetActive(false);
            yield return Tips.DOFillAmount(0,time);
            yield return new WaitForSeconds(time);
            endAction?.Invoke();
            Close();
            Tips.fillOrigin = (int)Image.OriginHorizontal.Left;
        }
    }
}

