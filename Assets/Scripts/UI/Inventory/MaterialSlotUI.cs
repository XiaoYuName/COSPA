using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace ARPG.UI
{
    public class MaterialSlotUI : UIBase
    {
        private TextMeshProUGUI Count;
        private Image icon;
        public override void Init()
        {
            icon = GetComponent<Image>();
            Count = Get<TextMeshProUGUI>("Count");
        }

        public void InitData(ItemBag bag)
        {
            Item item = InventoryManager.Instance.GetItem(bag.ID);
            Count.gameObject.SetActive(true);
            Count.text = bag.count.ToString();
            icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);;
        }


        public void InitData(Item item)
        {
            icon.sprite =GameSystem.Instance.GetSprite(item.spriteID);
            Count.gameObject.SetActive(false);
        }
    }
}

