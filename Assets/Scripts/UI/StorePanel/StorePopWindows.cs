using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using DG.Tweening;
using TMPro;
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
        private TextMeshProUGUI titleText;
        
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
            titleText = Get<TextMeshProUGUI>("UIMask/Back/Top/title");
        }
        /// <summary>
        /// 切換與生產商城Item
        /// </summary>
        public void SwitchCreatStoreItemUI(StoreType type)
        {
            UIHelper.Clear(content);
            List<StoreItem> storeItems = data.GetTypeStore(type);
            titleText.text = type+"商店";
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

