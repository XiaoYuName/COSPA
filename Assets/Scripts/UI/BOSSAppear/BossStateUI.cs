using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class BossStateUI : UIBase
    {
        private Slider _slider;
        private TextMeshProUGUI Hp;
        private RectTransform _fill; 
        private RectTransform _tweenFill;
        
        private float tween_speed = 1.0f;
        private bool tween_flag;
        private float last_max_x; 
        private float start_x;     // 缓慢掉血的血条-缓动起点
        private float end_x;       // 缓慢掉血的血条-缓动终点
        private float now_x;       
        private float tm_t;
        private Coroutine _Tween;
        
        public override void Init()
        {
            _slider = Get<Slider>("UIMask/Slider");
            _fill = Get<RectTransform>("UIMask/Slider/Fill Area/Fill");
            _tweenFill = Get<RectTransform>("UIMask/Slider/Fill Area/Fill_Vlaue");
            Hp = Get<TextMeshProUGUI>("UIMask/Slider/HP_Value");
        }

        public void InitData(CharacterState state)
        {
            if (!isOpen)
                Open();
            _slider.minValue = 0;
            _slider.maxValue = state.HP;
            _slider.value = state.currentHp;
            Hp.text = state.currentHp + "/" + state.HP;
        }

        public void UpdateSlider(CharacterState enemy)
        {
            if (!isOpen)
            {
                InitData(enemy);
                Open();
            }

            if (_Tween != null)
            {
                StopCoroutine(_Tween);
                _tweenFill.anchorMax = new Vector2(_fill.anchorMax.x, _fill.anchorMax.y);
                _Tween = null;
            }
            _slider.value = enemy.currentHp;
            Hp.text = enemy.currentHp + "/" + enemy.HP;
            if(gameObject.activeSelf)
                _Tween = StartCoroutine(TweenFill());
            
        }

        private IEnumerator TweenFill()
        {
            start_x = last_max_x;        
            end_x = _fill.anchorMax.x;
            tween_flag = true;
            tm_t = 0;
            while (tween_flag && gameObject.activeSelf)
            {
                tm_t += tween_speed * Time.deltaTime;
                if (tm_t >= 1)
                {
                    tm_t = 1;
                    tween_flag = false;   // 关闭过渡效果
                    last_max_x = end_x;   // 记录缓动停止到哪里
                }
                // 采用Lerp, 暴击的时候, 会显得血掉的快
                now_x = Mathf.Lerp(start_x, end_x, tm_t);
                _tweenFill.anchorMax = new Vector2(now_x, _fill.anchorMax.y);
                yield return null;
            }
        }

        public override void Close()
        {
            base.Close();
            if (_Tween != null)
            {
                StopCoroutine(_Tween);
                _Tween = null;
            }

        }
    }
}

