using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace ARPG.UI
{
    public class MaterialSlotUI : UIBase,IPointerDownHandler,IPointerUpHandler
    {
        private TextMeshProUGUI Count;
        private Image icon;
        private string currentID;
        public override void Init()
        {
            icon = GetComponent<Image>();
            Count = Get<TextMeshProUGUI>("Count");
        }
        
        public void InitData(ItemBag bag)
        {
            Item item = InventoryManager.Instance.GetItem(bag.ID);
            Count.gameObject.SetActive(true);
            currentID = bag.ID;
            Count.text = bag.count.ToString();
            icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);;
        }
        
        public void InitData(Item item)
        {
            icon.sprite =GameSystem.Instance.GetSprite(item.spriteID);
            currentID = item.ID;
            Count.gameObject.SetActive(false);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if(!String.IsNullOrEmpty(currentID))
             UISystem.Instance.ShowPopItem(IDType.物品,currentID);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            UISystem.Instance.ClosePopItem();
        }
    }
}

