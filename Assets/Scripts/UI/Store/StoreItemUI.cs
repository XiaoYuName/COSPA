using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 商城物品Item
    /// </summary>
    public class StoreItemUI : UIBase
    {
        private Image BG;
        private TextMeshProUGUI infoText;
        private TextMeshProUGUI titleText;
        private Button GoBtn;

        private StoreItem currentdata;
        public override void Init()
        {
            BG = Get<Image>("BG");
            infoText = Get<TextMeshProUGUI>("BG/description");
            titleText = Get<TextMeshProUGUI>("info/Text (TMP)");
            GoBtn = Get<Button>("GoBtn");
            Bind(GoBtn,OnChick,"OnChick");
        }


        public void InitData(StoreItem item)
        {
            BG.sprite = item.BG;
            infoText.text = item.description;
            titleText.text = item.RMB.ToString();
            currentdata = item;
        }


        private void OnChick()
        {
            if (currentdata == null) return;
            UISystem.Instance.ShowPopDialogue("购买宝石","要购买宝石，确定吗","关闭","确定",null,
                delegate
                {
                    InventoryManager.Instance.AddGold(GoldType.宝石,currentdata.RMB);
                    UISystem.Instance.ShowPopWindows("提示","购买成功","成功");
                });
        }

    }

}
