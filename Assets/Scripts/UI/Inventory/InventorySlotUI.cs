using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class InventorySlotUI : UIBase
    {
        private Image icon;
        private TextMeshProUGUI Count;
        private TextMeshProUGUI Level;
        private TextMeshProUGUI Powor;
        private Button Btn;
        
        
        //--数据--
        private ItemBag currentItemBag;
        private bool isDistable;
        
        public override void Init()
        {
            icon = GetComponent<Image>();
            Btn = GetComponent<Button>();
            Count = Get<TextMeshProUGUI>("Count");
            Level = Get<TextMeshProUGUI>("Level");
            Powor = Get<TextMeshProUGUI>("Powor");
            Bind(Btn,OnClikc ,"UI_click");
           
        }

        public void InitData(ItemBag data)
        {
            if (!isOpen)
            {
                Open();
            }
            currentItemBag = data;
            Item item = InventoryManager.Instance.GetItem(data.ID);
            if (item.Type == ItemType.材料 || item.Type == ItemType.记忆碎片)
            {
                icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);
                Count.text = data.count.ToString("N0");
                Powor.gameObject.SetActive(false);
                Level.gameObject.SetActive(false);
            }
            else
            {
                icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);
                Count.text = data.count.ToString("N0");
                Powor.text = "+"+data.power.ToString("N0");
                Level.text = item.level.ToString("N0");
            }

            isDistable = false;
        }

        
        /// <summary>
        /// 刷新显示数据
        /// </summary>
        /// <param name="data">数据类型</param>
        /// <param name="DesitonOnClick">是否关闭单击事件</param>
        public void InitData(ItemBag data, bool DesitonOnClick)
        {
            InitData(data);
            isDistable = DesitonOnClick;
        }


        protected virtual void OnClikc()
        {
            //ItemShoToolitip()
            if (currentItemBag == null|| isDistable) return;
             InventoryUI ui  =  UISystem.Instance.GetUI<InventoryUI>("InventoryUI");
             ui.ShowItemToolTip(currentItemBag);
        }
    }

}
