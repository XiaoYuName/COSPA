using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace ARPG.UI
{
    public class MaterialSlotUI : UIBase
    {
        private TextMeshProUGUI Count;
        private Image icon;
        public override void Init()
        {
            icon = GetComponent<Image>();
            Count = Get<TextMeshProUGUI>("Count");
        }

        public void InitData(ItemBag bag)
        {
            Item item = InventoryManager.Instance.GetItem(bag.ID);
            Count.text = bag.count.ToString();
            icon.sprite = item.icon;
        }
        //TODO: 给武器添加显示详情弹窗
    }
}

