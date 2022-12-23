using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace ARPG.UI
{
    /// <summary>
    /// 倒计时控制器
    /// </summary>
    public class DownTime : UIBase
    {
        private TextMeshProUGUI DownTimeText;
        private WaitForSeconds _seconds;
        private Coroutine _coroutine;
        public override void Init()
        {
            DownTimeText = Get<TextMeshProUGUI>("DownTime");
            _seconds = new WaitForSeconds(1);
        }

        public void DownTiem(float time, Action func)
        {
            Open();
            _coroutine = StartCoroutine(DownTimeWait(time, func));
        }
        
        IEnumerator DownTimeWait(float time,Action func)
        {
            float down = time;
            DownTimeText.text = TextAnimaSettings.GetSpineText("yellowNumber", "S");
            yield return _seconds;
            while (down > 0)
            {
                down -= 1;
                DownTimeText.text = TextAnimaSettings.GetSpineText("yellowNumber", down.ToString(CultureInfo.InvariantCulture));
                yield return _seconds;
            }
            DownTimeText.text = TextAnimaSettings.GetSpineText("yellowNumber", "D");
            yield return _seconds;
            func?.Invoke();
            Close();
        }
        
        public override void Close()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            base.Close();
            
        }
    }
}

