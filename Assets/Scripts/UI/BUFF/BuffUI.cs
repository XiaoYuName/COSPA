using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class BuffUI : UIBase,IPointerEnterHandler,IPointerExitHandler
    {
        private Image icon;
        private TextMeshProUGUI Name;
        private TextMeshProUGUI Level;
        private Image FillAmount;
        private GameObject Descriptioninfo;
        private TextMeshProUGUI Description;
        public override void Init()
        {
            icon = Get<Image>("icon");
            Name = Get<TextMeshProUGUI>("BuffName");
            Level = Get<TextMeshProUGUI>("BuffLevel");
            FillAmount = Get<Image>("FillAmount");
            Descriptioninfo = Get("Descriptioninfo");
            Description = Get<TextMeshProUGUI>("Descriptioninfo/Description");
        }

        public void InitData(BuffData data)
        {
            icon.sprite = GameSystem.Instance.GetSprite(data.SpriteID);
            Name.text = data.BuffName;
            Level.gameObject.SetActive(data.behaviourType != BuffBehaviourType.光环);
            Level.text = "1";
            Description.text = data.description;
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

        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Descriptioninfo.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(Descriptioninfo.GetComponent<RectTransform>());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Descriptioninfo.gameObject.SetActive(false);
            LayoutRebuilder.ForceRebuildLayoutImmediate(Descriptioninfo.GetComponent<RectTransform>());
        }
    }
}

