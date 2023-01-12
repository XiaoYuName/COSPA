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
            Item item = InventoryManager.Instance.GetItem(data.ID);
            if (item.Type == ItemType.材料 || item.Type == ItemType.记忆碎片)
            {
                icon.sprite = item.icon;
                Count.text = data.count.ToString("N0");
                Powor.gameObject.SetActive(false);
                Level.gameObject.SetActive(false);
            }
            else
            {
                icon.sprite = item.icon;
                Count.text = data.count.ToString("N0");
                Powor.text = data.power.ToString("N0");
                Level.text = item.level.ToString("N0");
            }
        }


        protected virtual void OnClikc()
        {
            //ItemShoToolitip()
        }
    }

}
