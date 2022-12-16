using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class ItemToolTip : UIBase
    {
        private TextMeshProUGUI ItemName;
        private Image Slot;
        private TextMeshProUGUI Count;
        private RectTransform content;
        private PropValue Obj;
        private Button CloseBtn;
        private Button BindBtn;
        
        public override void Init()
        {
            ItemName = Get<TextMeshProUGUI>("Mask/Name");
            Slot = Get<Image>("Mask/SlotUI");
            Count = Get<TextMeshProUGUI>("Mask/SlotUI/Count");
            Obj = UISystem.Instance.GetPrefab<PropValue>("PropValue");
            content = Get<RectTransform>("Mask/Panel/Info/Scroll View/Content");
            CloseBtn = Get<Button>("Mask/Panel/CloseBtn");
            BindBtn = Get<Button>("Mask/Panel/BindBtn");
        }

        public void InitData(ItemBag itemBag)
        {
            Item item = InventoryManager.Instance.GetItem(itemBag.ID);
            ItemName.text = item.ItemName;
            Slot.sprite = item.icon;
            Count.text = "*" + itemBag.count;
            UIHelper.Clear(content);
            foreach (var t in item.attribute)
            {
                PropValue value =Instantiate(Obj, content);
                value.Init();
                value.Show(t.Mode.ToString(),t.value.ToString());
            }
            Bind(CloseBtn,Close,"OutChick");
            Open();
        }
    }
}

