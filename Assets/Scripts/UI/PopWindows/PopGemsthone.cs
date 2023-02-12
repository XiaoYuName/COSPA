using System;
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
    /// 消耗弹窗(宝石=>玛那)
    /// </summary>
    public class PopGemsthone : UIBase
    {
        private TextMeshProUGUI Title;
        private TextMeshProUGUI description;
        private TextMeshProUGUI GemsthoneName;
        private GameObject GemsthoneImage;
        private GameObject MaNaImage;
        private TextMeshProUGUI GemsBagValue;
        private TextMeshProUGUI GemsAmount;

        private GameObject PropPanel;
        private TextMeshProUGUI PropName;
        private TextMeshProUGUI PropBagValue;
        private TextMeshProUGUI PropAmount;

        private Button CloseBtn;
        private Button FuncBtn;
        
        
        public override void Init()
        {
            Title = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Top/TitleText");
            description = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Center/description");
            GemsthoneName = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Center/Gemsthone/PropName");
            GemsthoneImage = Get("UIMask/Back/Back/Farme/Center/Gemsthone/Gemsthone");
            MaNaImage = Get("UIMask/Back/Back/Farme/Center/Gemsthone/MaNa");
            GemsBagValue = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Center/Gemsthone/BagValue");
            GemsAmount = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Center/Gemsthone/Amount");
            PropPanel = Get("UIMask/Back/Back/Farme/Center/PT_Name");
            PropName = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Center/PT_Name/PropName");
            PropBagValue = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Center/PT_Name/BagValue");
            PropAmount = Get<TextMeshProUGUI>("UIMask/Back/Back/Farme/Center/PT_Name/Amount");
            CloseBtn = Get<Button>("UIMask/Back/Back/Farme/Down/CloseBtn");
            FuncBtn = Get<Button>("UIMask/Back/Back/Farme/Down/FuncBtn");
            Bind(CloseBtn,Close,UiAudioID.OutChick);
        }

        /// <summary>
        /// 显示消耗弹窗(单消耗)
        /// </summary>
        /// <param name="titleName">标题名称</param>
        /// <param name="description">提示内容</param>
        /// <param name="goldType">消耗货币类型</param>
        /// <param name="Amount">消耗数量</param>
        /// <param name="funcBtn">点击确认后Button回调</param>
        public void Show(string titleName, string description, GoldType goldType,int Amount, Action funcBtn)
        {
            Open();
            Title.text = titleName;
            this.description.text = description;
            GemsthoneName.text = "消耗"+goldType;
            GemsthoneImage.gameObject.SetActive(goldType == GoldType.宝石);
            MaNaImage.gameObject.SetActive(goldType == GoldType.玛那);
            int itemCount = InventoryManager.Instance
                .GetItemBag(goldType == GoldType.宝石 ? Settings.GemsthoneID : Settings.ManaID).count;
            GemsBagValue.text = itemCount.ToString();
            GemsAmount.text = Mathf.Max(itemCount - Amount, 0).ToString();
            PropPanel.gameObject.SetActive(false);
            Bind(FuncBtn, () =>
            {
                Close();
                funcBtn?.Invoke();
            },UiAudioID.OnChick);
        }
        
        /// <summary>
        /// 显示消耗弹窗(单消耗+加成)
        /// </summary>
        /// <param name="titleName">标题名称</param>
        /// <param name="description">提示内容</param>
        /// <param name="goldType">消耗货币类型</param>
        /// <param name="Amount">消耗数量</param>
        /// <param name="PropName">加成名称</param>
        /// <param name="PropID">加成ID</param>
        /// <param name="PropAmount">加成数量</param>
        /// <param name="funcBtn">点击确认后Button回调</param>
        public void Show(string titleName, string description, GoldType goldType,int Amount,string PropName,
            string PropID,int PropAmount,Action funcBtn)
        {
            Show(titleName,description,goldType,Amount,funcBtn);
            PropPanel.gameObject.SetActive(true);
            this.PropName.text = PropName;
            int itemCount = 0;
            ItemBag bag = InventoryManager.Instance.GetItemBag(PropID);
            if (bag!= null)
            {
                itemCount = bag.count;
            }
            
            PropBagValue.text = itemCount.ToString();
            this.PropAmount.text = Mathf.Max(itemCount + PropAmount, 0).ToString();
            
        }

        public override void Open()
        {
            base.Open();
            transform.DOScale(Vector3.one, Settings.PopTweenTime);
            AudioManager.Instance.PlayAudio(UiAudioID.PopWindows);
        }

        public override void Close()
        {
            transform.DOScale(Vector3.zero, Settings.PopTweenTime).OnComplete(() => base.Close());
        }
    }
}

