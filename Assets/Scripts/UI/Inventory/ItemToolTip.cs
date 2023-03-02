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
        private TextMeshProUGUI BindBtnName;
        
        public override void Init()
        {
            ItemName = Get<TextMeshProUGUI>("Mask/Name");
            ItemType = Get<TextMeshProUGUI>("Mask/Type");
            Slot = Get<Image>("Mask/SlotUI");
            Count = Get<TextMeshProUGUI>("Mask/SlotUI/Count");
            Level = Get<TextMeshProUGUI>("Mask/SlotUI/Level");
            Powor = Get<TextMeshProUGUI>("Mask/SlotUI/Powor");
            Obj = UISystem.Instance.GetPrefab<PropValue>("StateValue");
            content = Get<RectTransform>("Mask/Panel/Info/Scroll View/Content");
            CloseBtn = Get<Button>("Mask/Panel/CloseBtn");
            BindBtn = Get<Button>("Mask/Panel/BindBtn");
            BindBtnName = BindBtn.transform.Find("BtnName").GetComponent<TextMeshProUGUI>();
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
            BindBtnName.text = "装备";
            Bind(BindBtn, SetEquip, "OnChick");
            Open();
        }

        public void InitData(EquipHeloUI hole)
        {
            ItemName.text = hole.currentdata.item.ItemName;
            ItemType.text = hole.currentdata.item.Type.ToString();
            Level.text = "lv: "+hole.currentdata.item.level;
            Powor.text = "+" +hole.currentdata.Powor;
            Slot.sprite = GameSystem.Instance.GetSprite(hole.currentdata.item.spriteID);
            Count.text = "*" + 1;
            UIHelper.Clear(content);
            foreach (var t in hole.currentdata.item.attribute)
            {
                PropValue value =Instantiate(Obj, content);
                value.Init();
                value.Show(t.Mode.ToString(),(t.value*Mathf.Max(1,hole.currentdata.Powor)).ToString());
            }
            Bind(CloseBtn,Close,"OutChick");
            BindBtnName.text = "卸下";
            Bind(BindBtn, ()=>UEquip(hole), "OnChick");
            Open();
        }

        private void SetEquip()
        {
            UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").UpdateEquipHolo(currentItem);
            Close();
        }

        //卸下装备
        private void UEquip(EquipHeloUI hole)
        {
            ItemBag itemBag = new ItemBag()
            {
                ID = hole.currentdata.item.ID,
                count = 1,
                power = hole.currentdata.Powor,
            };
            InventoryManager.Instance.AddItem(itemBag);
            hole.currentdata.item = new Item();
            hole.InitData(hole.currentdata);
            CharacterEquipPanel baseUI = UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel");
            InventoryManager.Instance.SendCharacterBag(baseUI.GetCurrentCharacterBag());
            UISystem.Instance.ShowTips("装备卸载成功");
        }
    }
}

