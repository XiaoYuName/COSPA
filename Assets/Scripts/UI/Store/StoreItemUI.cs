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
            titleText.text = item.RMB.ToString("C");
            currentdata = item;
        }


        private void OnChick()
        {
            if (currentdata == null) return;
            GoldType type = currentdata.Type switch
            {
                StoreType.宝石 => GoldType.宝石,
                StoreType.玛娜 => GoldType.玛那,
                _ => GoldType.宝石
            };

                UISystem.Instance.ShowPopDialogue("购买物品","要购买"+type+",确定吗","关闭","确定",null,
                delegate
                {
                    InventoryManager.Instance.AddGold(type,currentdata.RewordCount);
                    TaskManager.Instance.TriggerTask(TaskTrigger.充值,currentdata.RMB);
                },true);
        }

    }

}
