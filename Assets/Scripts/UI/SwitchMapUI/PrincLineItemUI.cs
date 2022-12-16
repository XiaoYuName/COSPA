using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PrincLineItemUI : UIBase
    {
        private GameObject Active;
        private TextMeshProUGUI PrincName;
        private Button StarGameBtn;
        /// <summary>
        /// 当前任务
        /// </summary>
        private PrincItem currentdata;
        /// <summary>
        /// 是否可点击
        /// </summary>
        private bool isClick;
        public override void Init()
        {
            Active = Get("Active");
            PrincName = Get<TextMeshProUGUI>("Text");
            StarGameBtn = GetComponent<Button>();
        }

        public void InitData(int index ,PrincItem data,int active)
        {
            PrincName.text = data.PrincItemName;
            SetStateAction(active);
            StarGameBtn.interactable = active != 3;
            Bind(StarGameBtn,delegate { print("进入场景"); },"OnChick" );
        }
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="index"> 1 代表已看完,2 代表新内容,3代表暂不能观看，未解锁</param>
        /// <param name="data"></param>
        public void InitData(int active,PrincLine data,int index)
        {
            PrincName.text = data.PrincName;
            SetStateAction(active);
            StarGameBtn.interactable = active != 3;
            Bind(StarGameBtn, delegate
            {
                SwicthMapPanel mapPanel =  UISystem.Instance.GetUI<SwicthMapPanel>("SwicthMapPanel");
                isClick = !isClick;
                mapPanel.SetAnimator(isClick);
                mapPanel.CreateLineItemChlidUI(index);
            }, "OnChick");
        }


        private void SetStateAction(int index)
        {
            switch (index)
            {
                case 1:
                    Active.transform.GetChild(0).gameObject.SetActive(false);
                    Active.transform.GetChild(1).gameObject.SetActive(false);
                    Active.transform.GetChild(2).gameObject.SetActive(true);
                    break;
                case 2:
                    Active.transform.GetChild(0).gameObject.SetActive(true);
                    Active.transform.GetChild(1).gameObject.SetActive(false);
                    Active.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case 3:
                    Active.transform.GetChild(0).gameObject.SetActive(false);
                    Active.transform.GetChild(1).gameObject.SetActive(true);
                    Active.transform.GetChild(2).gameObject.SetActive(false);  
                    break;
            }
            
            
        }
    }
}

