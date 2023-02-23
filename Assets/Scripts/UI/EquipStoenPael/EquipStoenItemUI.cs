using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class EquipStoenItemUI : UIBase
    {
        private MaterialSlotUI Rewordicon;
        private TextMeshProUGUI RewordItemName;
        private TextMeshProUGUI ItemBagAmount;//持有数
        
        private TextMeshProUGUI SubItemAmount;
        private GameObject Back;
        private Button BindBtn;

        private EquipStoenData _data;
        public override void Init()
        {
            Rewordicon = Get<MaterialSlotUI>("MaterialSlotUI");
            Rewordicon.Init();
            RewordItemName = Get<TextMeshProUGUI>("ItemName");
            ItemBagAmount = Get<TextMeshProUGUI>("BagAmount");
            SubItemAmount = Get<TextMeshProUGUI>("Back/Number");
            Back = Get("Back");
            BindBtn = Get<Button>("BindBtn");
            Bind(BindBtn,OnClick,UiAudioID.OnChick);
        }

        public void InitData(EquipStoenData data)
        {
            _data = data;
            Item item = InventoryManager.Instance.GetItem(data.RewordItem.ID);
            if (item == null) return;
            Rewordicon.InitData(data.RewordItem);
            RewordItemName.text = item.ItemName;
            ItemBag itemBag = InventoryManager.Instance.GetItemBag(data.RewordItem.ID);
            ItemBagAmount.text = itemBag == null ? "持有数：0" : "持有数："+itemBag.count;
            Back.transform.Find("Type").GetChild((int)data.StoenGoldIconType).gameObject.SetActive(true);
            Item SubItem = InventoryManager.Instance.GetItem(data.SubItem.ID);
            if (data.StoenGoldIconType == StoenGoldIconType.材料)
            {
                Back.transform.Find("Type").GetChild(0).transform.GetComponent<Image>().sprite = GameSystem.Instance.GetSprite(SubItem.spriteID);
            }
            SubItemAmount.text = data.SubItem.count.ToString("N0");
            InventoryManager.Instance.RegAddItemAmount(data.RewordItem.ID,BindItemRegion);
        }


        public void OnClick()
        {
            if (_data == null) return;
            ItemBag itemBag = InventoryManager.Instance.GetItemBag(_data.SubItem.ID);

            if (itemBag == null || itemBag.count < _data.SubItem.count)
            {
                UISystem.Instance.ShowPopWindows("提示","购买所需材料不足","关闭");
                return;
            }
            InventoryManager.Instance.DeleteItemBag(_data.SubItem.ID,_data.SubItem.count);

            ItemBag newBag = new ItemBag()
            {
                ID = _data.RewordItem.ID,
                count = _data.RewordItem.count,
                power = _data.RewordItem.power,
            };
            InventoryManager.Instance.AddItem(newBag);
            UISystem.Instance.ShowReword(newBag);

        }

        public void BindItemRegion(ItemBag itemBag)
        {
            ItemBagAmount.text = "持有数："+itemBag.count;
        }

        public void OnDestroy()
        {
            if(_data != null)
                InventoryManager.Instance.URegItemAmount(_data.RewordItem.ID,BindItemRegion);
        }
    }
}

