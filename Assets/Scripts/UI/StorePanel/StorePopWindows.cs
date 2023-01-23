using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 宝石商店弹窗
    /// </summary>
    public class StorePopWindows : UIBase
    {
        private Button CloseBtn;
        private RectTransform content;
        
        /// <summary>
        /// 商城配置
        /// </summary>
        private StoreConfig data;
        

        public override void Init()
        {
            transform.localScale = Vector3.zero;
            CloseBtn = Get<Button>("UIMask/Back/Close");
            Bind(CloseBtn, Close, "OutChick");
            content = Get<RectTransform>("UIMask/Back/Scroll View/Viewport/Content");
            data = ConfigManager.LoadConfig<StoreConfig>("Store/Store");
            CreatStoreItemUI();
        }


        /// <summary>
        /// 生成StoreItemUI
        /// </summary>
        private void CreatStoreItemUI()
        {
            List<StoreItem> storeItems = data.GetTypeStore(StoreType.宝石);

            foreach (var t in storeItems)
            {
                StoreItemUI itemUI =  UISystem.Instance.InstanceUI<StoreItemUI>("StoreItemUI",content);
                itemUI.InitData(t);
            }
        }
        
        
        public override void Close()
        {
            base.Close();
            transform.localScale  = Vector3.zero;
        }

        public override void Open()
        {
            base.Open();
            transform.DOScale(new Vector3(1, 1, 1), 0.25f).SetEase(Ease.Linear);
        }
    }
}

