using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class InventoryUI : UIBase
    {
        private MoneyUI _moneyUI;
        private InventorySlotUI _InventorySlotUI;
        private RectTransform content;
        private InventoryItemToolTip itemToolTip;
        private Button CloseBtn;
        
        /// <summary>
        /// 当前选中分页
        /// </summary>
        private ItemType CurrentTable;

        /// <summary>
        /// 材料Btn
        /// </summary>
        private Button MaterialBtn;
        /// <summary>
        /// 装备Btn
        /// </summary>
        private Button EquitBtn;
        /// <summary>
        /// 记忆碎片Btn
        /// </summary>
        private Button HeadBtn;

        //装备UI列表
        private List<InventorySlotUI> ItemEquipSlot = new List<InventorySlotUI>();
        //材料UI列表
        private List<InventorySlotUI> ItemMaterialSlot= new List<InventorySlotUI>();
        //记忆碎片UI列表
        private List<InventorySlotUI> ItemHeadSlotUis= new List<InventorySlotUI>();

        public override void Init()
        {
            _moneyUI = Get<MoneyUI>("UIMask/MoneyUI");
            _moneyUI.Init();
            _InventorySlotUI = UISystem.Instance.GetPrefab<InventorySlotUI>("InventorySlotUI");
            content = Get<RectTransform>("UIMask/Slot/Scroll View/Viewport/Content");
            MaterialBtn = Get<Button>("UIMask/SwitchTable/StateBtn");
            EquitBtn = Get<Button>("UIMask/SwitchTable/EquipBtn");
            HeadBtn = Get<Button>("UIMask/SwitchTable/HeadBtn");
            Bind(MaterialBtn,()=>SwitchTable(ItemType.材料),"OnChick");
            Bind(EquitBtn,()=>SwitchTable(ItemType.武器),"OnChick");
            Bind(HeadBtn,()=>SwitchTable(ItemType.记忆碎片),"OnChick");
            itemToolTip = Get<InventoryItemToolTip>("UIMask/Slot/InventoryItemToolTip");
            itemToolTip.Init();
            SwitchTable(ItemType.材料);
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OutChick");
            MessageAction.RefreshItemBag += CreatItemBages;
            CreatInventorySlotUI();
        }

  

        /// <summary>
        /// 创建生成ItemUI，ItemUI点击后显示ItemToolTip
        /// </summary>
        private void CreatInventorySlotUI()
        {
            //获取背包中所有的Item
            CreatItemBages(InventoryManager.Instance.GetItemAllBag());
        }

        private void CreatItemBages(List<ItemBag> itemBags)
        {
            UIHelper.Clear(content);
            ItemEquipSlot.Clear();
            ItemMaterialSlot.Clear();
            ItemHeadSlotUis.Clear();
            if (itemBags.Count <= 0) return;
            for (int i = 0; i < itemBags.Count; i++)
            {
                InventorySlotUI slotUI = Instantiate(_InventorySlotUI, content);
                Item item = InventoryManager.Instance.GetItem(itemBags[i].ID);
                slotUI.Init();
                slotUI.InitData(itemBags[i]);
                if (item.Type == ItemType.材料)
                {
                    ItemMaterialSlot.Add(slotUI);
                }else if(item.Type == ItemType.记忆碎片)
                {
                    ItemHeadSlotUis.Add(slotUI);
                }else
                {
                    ItemEquipSlot.Add(slotUI);
                }
            }
            SwitchTable(CurrentTable);
        }



        /// <summary>
        /// 选择当前选中页
        /// </summary>
        /// <param name="table"></param>
        private void SwitchTable(ItemType table)
        {
            itemToolTip.Close();
            MaterialBtn.GetComponent<Image>().color = table == ItemType.材料 ? Color.white : new Color(1, 1, 1, 0);
            EquitBtn.GetComponent<Image>().color =
                table != ItemType.材料 && table != ItemType.记忆碎片 ? Color.white : new Color(1, 1, 1, 0);
            HeadBtn.GetComponent<Image>().color = table == ItemType.记忆碎片 ? Color.white : new Color(1, 1, 1, 0);
            foreach (var eSlotUI in ItemEquipSlot)
            {
                eSlotUI.Close();
            }
            foreach (var eSlotUI in ItemMaterialSlot)
            {
                eSlotUI.Close(); 
            }
            foreach (var eSlotUI in ItemHeadSlotUis)
            {
                eSlotUI.Close();
            }
            switch (table)
            {
                case ItemType.材料:
                    foreach (var eSlotUI in ItemMaterialSlot)
                    {
                        eSlotUI.Open(); 
                    }
                    break;
                case ItemType.记忆碎片:
                    foreach (var eSlotUI in ItemHeadSlotUis)
                    {
                        eSlotUI.Open();
                    }
                    break;
                default:
                    foreach (var eSlotUI in ItemEquipSlot)
                    {
                        eSlotUI.Open();
                    }
                    break;
            }
        }


        public void ShowItemToolTip(ItemBag data)
        {
            itemToolTip.InitData(data);
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("InventoryUI");
        }
    }
}

