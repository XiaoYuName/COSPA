using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 显示物品详情
    /// </summary>
    public class InventoryItemToolTip : UIBase
    {
        private TextMeshProUGUI itemName;
        private TextMeshProUGUI itemType;
        private TextMeshProUGUI itemMode;
        private InventorySlotUI slotUI;
        private RectTransform StateConent;
        private TextMeshProUGUI description;
        private TextMeshProUGUI SellAmount;
        private Button SellBtn;
        // private PropValue 

        private ItemBag currentBag;
        public override void Init()
        {
            itemName = Get<TextMeshProUGUI>("UIMask/Top/ItemName");
            itemType = Get<TextMeshProUGUI>("UIMask/Top/ItemType");
            slotUI = Get<InventorySlotUI>("UIMask/Top/InventorySlotUI");
            StateConent = Get<RectTransform>("UIMask/Crent/Scroll View/Viewport/StateContent");
            description = Get<TextMeshProUGUI>("UIMask/Top/description/Value");
            SellAmount = Get<TextMeshProUGUI>("UIMask/Down/State_Value/ItemSell");
            SellBtn = Get<Button>("UIMask/Down/SellBtn");
            itemMode = Get<TextMeshProUGUI>("UIMask/Top/itemMode");
            slotUI.Init();
            Bind(SellBtn,OnClick,"OnChick");
        }

        /// <summary>
        /// 显示UI数据
        /// </summary>
        /// <param name="itemBag"></param>
        public void InitData(ItemBag itemBag)
        {
            if (!isOpen)
            {
                Open();
            }
            Item item = InventoryManager.Instance.GetItem(itemBag.ID);
            itemName.text = item.ItemName;
            itemType.text = item.Type.ToString();
            itemMode.text = item.Mode.ToString();
            slotUI.InitData(itemBag,true);
            description.text = item.description;
            SellAmount.text = item.sellAmount.ToString();
            UIHelper.Clear(StateConent);
            foreach (var stateValue in item.attribute)
            {
                PropValue propValue = UISystem.Instance.InstanceUI<PropValue>("InventoryPropValue",StateConent);
                propValue.Show(stateValue.Mode.ToString(),(stateValue.value*Mathf.Max(1,itemBag.power)).ToString());
            }
            SellBtn.interactable = item.isSell;
            currentBag = itemBag;
        }


        protected virtual void OnClick()
        {
            if (currentBag == null) return;
            UISystem.Instance.ShowPopDialogue("提示","确定要出售该物品吗","确定","取消",Sell,null);
            //出售后关闭自身
        }

        protected virtual void Sell()
        {
            Item item = InventoryManager.Instance.GetItem(currentBag.ID);
            int ManaAmount = item.sellAmount;
            InventoryManager.Instance.DeleteItemBag(currentBag,1);
            InventoryManager.Instance.AddGold(GoldType.玛那,ManaAmount);
            Close();
        }
    }

}
