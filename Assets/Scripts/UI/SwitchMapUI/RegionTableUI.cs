using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class RegionTableUI : UIBase
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
        private Image Regionicon;
        public override void Init()
        {
            Active = Get("Active");
            PrincName = Get<TextMeshProUGUI>("Text");
            StarGameBtn = Get<Button>("Mask");
            Regionicon = Get<Image>("Mask/BG");
        }

        public void InitData(int index ,RegionItem data,int active)
        {
            PrincName.text = data.RegionItemName;
            SetStateAction(active);
            StarGameBtn.interactable = active != 3;
            Regionicon.sprite = data.backIcon;
            Bind(StarGameBtn,delegate { print("进入场景"); },"OnChick" );
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="index"> 1 代表已看完,2 代表新内容,3代表暂不能观看，未解锁</param>
        /// <param name="active">是否激活</param>
        /// <param name="data">数据文件</param>
        public void InitData(int active,RegionLine data,int index)
        {
            PrincName.text = data.RegionName;
            SetStateAction(active);
            Regionicon.sprite = data.backIcon;
            StarGameBtn.interactable = active != 3;
            Bind(StarGameBtn, delegate
            {
                RegionPanel mapPanel =  UISystem.Instance.GetUI<RegionPanel>("RegionPanel");
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

