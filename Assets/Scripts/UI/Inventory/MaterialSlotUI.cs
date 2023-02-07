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
        private TextMeshProUGUI PowerText;
        private TextMeshProUGUI LevelText;
        public override void Init()
        {
            icon = GetComponent<Image>();
            Count = Get<TextMeshProUGUI>("Count");
            PowerText = Get<TextMeshProUGUI>("Power");
            LevelText = Get<TextMeshProUGUI>("Level");
        }
        
        public void InitData(ItemBag bag)
        {
            Item item = InventoryManager.Instance.GetItem(bag.ID);
            Count.gameObject.SetActive(true);
            currentID = bag.ID;
            Count.text = bag.count.ToString();
            icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);
            if (InventoryManager.Instance.isEquip(item))
            {
                PowerText.gameObject.SetActive(true);
                LevelText.gameObject.SetActive(true);
                PowerText.text = "+"+bag.power;
                LevelText.text = "lv:" + item.level;
            }
            else
            {
                PowerText.gameObject.SetActive(false);
                LevelText.gameObject.SetActive(false);
            }

        }
        
        public void InitData(Item item)
        {
            icon.sprite =GameSystem.Instance.GetSprite(item.spriteID);
            currentID = item.ID;
            Count.gameObject.SetActive(false);
            PowerText.gameObject.SetActive(false);
            LevelText.gameObject.SetActive(false);
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

