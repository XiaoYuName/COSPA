using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG
{
    public class FadeManager : Singleton<FadeManager>
    {
        private CanvasGroup FadeGroup;
        private bool isFade;
        private Action endfun;
        private FadeConfig _config;
        private Image SpriteIcon;
        protected override void Awake()
        {
            base.Awake();
            _config = ConfigManager.LoadConfig<FadeConfig>("Fade/FadeConfig");
            SpriteIcon = transform.Find("Icon").GetComponent<Image>();
            FadeGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                PlayFade(1,delegate { Debug.Log("Fade中间"); },2.5f);
            }
        }


        /// <summary>
        /// 播放一个淡入淡出
        /// </summary>
        /// <param name="time">淡入淡出的总时长</param>
        /// <param name="endfunc">淡入结束后的事件</param>
        /// <param name="stoptime">淡入后停顿多少秒开始淡出</param>
        public void PlayFade(float time, Action endfunc,float stoptime)
        {
            if (isFade) return;
            endfun = endfunc;
            SpriteIcon.sprite = _config.GetRandomSprite();
            StartCoroutine(StarFade(time,stoptime));
        }

        private IEnumerator StarFade(float time,float stoptime)
        {
            yield return Fade(1,time);
            endfun?.Invoke();
            yield return new WaitForSeconds(stoptime);
            
            yield return Fade(0,time);
            FadeGroup.blocksRaycasts = false;
            endfun = null;
        }

        private IEnumerator Fade(float targetAlpha,float tiem)
        {
            isFade = true;
            FadeGroup.blocksRaycasts = true;
            float speed = Mathf.Abs(FadeGroup.alpha - targetAlpha)/ tiem;
            while (!Mathf.Approximately(FadeGroup.alpha,targetAlpha))
            {
                FadeGroup.alpha = Mathf.MoveTowards(FadeGroup.alpha, targetAlpha, speed * UnityEngine.Time.deltaTime);
                yield return null;
            }
            isFade = false;
        }
    }
}

