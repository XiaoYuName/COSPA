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
        private TextMeshProUGUI ItemType;
        private Image Slot;
        private TextMeshProUGUI Count;
        private TextMeshProUGUI Level;
        private TextMeshProUGUI Powor;
        private RectTransform content;
        private PropValue Obj;
        private Button CloseBtn;
        private Button BindBtn;
        private ItemBag currentItem;
        
        public override void Init()
        {
            ItemName = Get<TextMeshProUGUI>("Mask/Name");
            ItemType = Get<TextMeshProUGUI>("Mask/Type");
            Slot = Get<Image>("Mask/SlotUI");
            Count = Get<TextMeshProUGUI>("Mask/SlotUI/Count");
            Level = Get<TextMeshProUGUI>("Mask/SlotUI/Level");
            Powor = Get<TextMeshProUGUI>("Mask/SlotUI/Powor");
            Obj = UISystem.Instance.GetPrefab<PropValue>("PropValue");
            content = Get<RectTransform>("Mask/Panel/Info/Scroll View/Content");
            CloseBtn = Get<Button>("Mask/Panel/CloseBtn");
            BindBtn = Get<Button>("Mask/Panel/BindBtn");
        }

        public void InitData(ItemBag itemBag)
        {
            Item item = InventoryManager.Instance.GetItem(itemBag.ID);
            currentItem = itemBag;
            ItemName.text = item.ItemName;
            ItemType.text = item.Type.ToString();
            Level.text = "lv: "+item.level;
            Powor.text = "+" +itemBag.power;
            Slot.sprite = GameSystem.Instance.GetSprite(item.spriteID);
            Count.text = "*" + itemBag.count;
            UIHelper.Clear(content);
            foreach (var t in item.attribute)
            {
                PropValue value =Instantiate(Obj, content);
                value.Init();
                value.Show(t.Mode.ToString(),(t.value*Mathf.Max(1,itemBag.power)).ToString());
            }
            Bind(CloseBtn,Close,"OutChick");
            Bind(BindBtn, SetEquip, "OnChick");
            Open();
        }

        private void SetEquip()
        {
            UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").UpdateEquipHolo(currentItem);
            Close();
        }
    }
}

