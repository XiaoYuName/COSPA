using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI
{
    public class StarContent : UIBase
    {
        public override void Init()
        {
            
        }


        /// <summary>
        /// 设置显示的星级
        /// </summary>
        /// <param name="star"></param>
        public void Show(int star)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                GameObject child = transform.GetChild(j).gameObject;
                child.transform.GetChild(0).gameObject.SetActive(j <= star - 1);
            }
        }
    }
}

