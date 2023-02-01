using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class BuffUI : UIBase
    {
        private Image icon;
        private TextMeshProUGUI Name;
        private TextMeshProUGUI Level;
        private Image FillAmount;
        public override void Init()
        {
            icon = Get<Image>("icon");
            Name = Get<TextMeshProUGUI>("BuffName");
            Level = Get<TextMeshProUGUI>("BuffLevel");
            FillAmount = Get<Image>("FillAmount");
        }

        public void InitData(BuffData data)
        {
            icon.sprite = GameSystem.Instance.GetSprite(data.SpriteID);
            Name.text = data.BuffName;
            Level.gameObject.SetActive(data.behaviourType != BuffBehaviourType.光环);
            Level.text = "1";
            if (data.behaviourType == BuffBehaviourType.光环)
            {
                Level.text = "99";
                FillAmount.fillAmount = 0;
                return;
            }
            
            StartCoroutine(FillAmountTween(data.continueTime));
        }

        /// <summary>
        /// 刷新UI显示
        /// </summary>
        public void RefUI(int level)
        {
            Level.text = level.ToString();
        }

        public IEnumerator FillAmountTween(float corontinetime)
        {
            FillAmount.fillAmount = 1;
            float time = corontinetime;
            while (gameObject.activeSelf)
            {
                time -= Time.deltaTime;
                if (time <= 0)
                {
                    time = corontinetime;
                    FillAmount.fillAmount = 1;
                }

                float fillAmount = Mathf.Min(((1/corontinetime)*Time.deltaTime),0.1f);
                FillAmount.fillAmount -= Mathf.Max(fillAmount, 0);
                yield return null;
            }
        }

    }
}

