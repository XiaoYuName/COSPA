using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class PowerUI : UIBase
    {
        private TextMeshProUGUI ItemName;
        private TextMeshProUGUI SliderValue;
        private Button PowerBtn;
        private Button AddIndexBtn;
        private Button SunIndexBtn;
        private MaterialSlotUI _slotUI;
        private List<ItemBag> CurrentEquips = new List<ItemBag>();
        private int curretnIndex;
        private int currentPoworAmount;

        public override void Init()
        {
            ItemName = Get<TextMeshProUGUI>("ItemName");
            SliderValue = Get<TextMeshProUGUI>("SliderValue");
            PowerBtn = Get<Button>("PowerBtn");
            AddIndexBtn = Get<Button>("SwitchBtns/AddBtn");
            SunIndexBtn = Get<Button>("SwitchBtns/SubBtn");
            _slotUI = Get<MaterialSlotUI>("MaterialSlotUI");
            _slotUI.Init();
            Bind(AddIndexBtn,()=>SetCurrentSlotUI(true),"OnChick");
            Bind(SunIndexBtn,()=>SetCurrentSlotUI(false),"OnChick");
            Bind(PowerBtn,PaworEquip,"OnChick");
            curretnIndex = 0;
        }

        public void InitData(List<ItemBag> Equips)
        {
            CurrentEquips = Equips;
            currentPoworAmount = 0;
            SetCurrentSlotUI(curretnIndex);
        }

        private void SetCurrentSlotUI(bool isAdd)
        {
            if (!isAdd)
            {
                if (curretnIndex != 0)
                    curretnIndex--;
                else
                    return;
            }
            else
            {
                if (curretnIndex < CurrentEquips.Count-1)
                    curretnIndex++;
                else
                    return;
            }
            SetCurrentSlotUI(curretnIndex);
        }

        private void SetCurrentSlotUI(int index)
        {
            curretnIndex = index;
            _slotUI.InitData(CurrentEquips[curretnIndex]);
            SetShow(CurrentEquips[curretnIndex]);
        }

        private void SetShow(ItemBag itemBag)
        {
            Item item = InventoryManager.Instance.GetItem(itemBag.ID);
            ItemName.text = "<color=red>+"+itemBag.power+"</color> "+item.ItemName;
            ItemBag PaworItem = InventoryManager.Instance.GetItemBag(Settings.PoworID);
            int PaworAmount = (int)(Settings.PaworDeftualAmount*item.level* MathF.Max(itemBag.power,1));
            currentPoworAmount = PaworAmount;
            if (PaworItem == null || PaworItem.count < 0)
            {
                SliderValue.text = "0" + "/" + PaworAmount;
            }
            else
            {
                SliderValue.text = PaworItem.count + "/" + PaworAmount;
            }
        }

        private void PaworEquip()
        {
            if (currentPoworAmount <= 0) return;
            ItemBag PaworItem = InventoryManager.Instance.GetItemBag(Settings.PoworID);
            if (PaworItem == null|| PaworItem.count < 0 || PaworItem.count < currentPoworAmount)
            {
                UISystem.Instance.ShowPopWindows("提示","材料不足","关闭",true);
                return;
            }
            UISystem.Instance.ShowPopDialogue("提示","是否消耗"+currentPoworAmount+"强化石强化",
                "确定","关闭",Pawor,null);
        }

        private void Pawor()
        {
            ItemBag itemBag = CurrentEquips[curretnIndex];
            itemBag.power++;
            InventoryManager.Instance.DeleteItemBag(Settings.PoworID, currentPoworAmount);
            UISystem.Instance.ShowPopWindows("提示","强化成功","关闭");
            MessageAction.OnRefreshItemBag(InventoryManager.Instance.GetItemAllBag());
        }
    }
}

