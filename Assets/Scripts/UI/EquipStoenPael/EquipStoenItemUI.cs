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
                Back.transform.GetChild(0).transform.GetComponent<Image>().sprite = GameSystem.Instance.GetSprite(SubItem.spriteID);
            }
            SubItemAmount.text = data.SubItem.count.ToString("N0");
        }


        public void OnClick()
        {
            if (_data == null) return;
            Debug.Log("点击");
        }
    }
}

