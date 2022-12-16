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
        public override void Init()
        {
            Active = Get("Active");
            PrincName = Get<TextMeshProUGUI>("Text");
            StarGameBtn = GetComponent<Button>();
        }

        public void InitData(PrincItem data)
        {
            PrincName.text = data.PrincItemName;
        }
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="index"> 1 代表已看完,2 代表新内容,3代表暂不能观看，未解锁</param>
        /// <param name="data"></param>
        public void InitData(int index,PrincLine data)
        {
            PrincName.text = data.PrincName;
            SetStateAction(index);
            StarGameBtn.interactable = index != 3;
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

