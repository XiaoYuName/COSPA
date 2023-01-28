using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.UI
{
    public class UIMaskManager : MonoSingleton<UIMaskManager>
    {
        private GameObject MainSceneMask;

        protected override void Awake()
        {
            base.Awake();
            MainSceneMask = transform.Find("UIMask/MainMask").gameObject;
        }

        /// <summary>
        /// 设置主界面Mask遮罩
        /// </summary>
        /// <param name="Active"></param>
        public void SetMainScnenMask(bool Active)
        {
            if(MainSceneMask.activeSelf != Active)
                MainSceneMask.gameObject.SetActive(Active);
        }      
    }
}

