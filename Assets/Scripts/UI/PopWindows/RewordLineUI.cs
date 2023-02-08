using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;

namespace ARPG.UI
{
    public class RewordLineUI : UIBase
    {
        private MaterialSlotUI SlotUI;
        private TextMeshProUGUI Text;
        public override void Init()
        {
            SlotUI = Get<MaterialSlotUI>("RewordSlotUI");
            Text = Get<TextMeshProUGUI>("description");
            SlotUI.Init();
        }

        public void InitData(ItemBag itemBag)
        {
             SlotUI.InitData(itemBag);
             Item item = InventoryManager.Instance.GetItem(itemBag.ID);
             if(InventoryManager.Instance.isEquip(item) && itemBag.power >0)
                 Text.text = "获得道具<color=yellow>+"+itemBag.power+"</color>"+"<color=red>"+ item.ItemName +"</color>*" + "<color=green>"+itemBag.count+"</color>";
             else
             {
                 Text.text = "获得道具<color=red>" + item.ItemName + "</color>*<color=green>" + itemBag.count+"</color>";
             }
        }
    }  
}

