using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SlotUI : UIBase,IPointerDownHandler,IPointerUpHandler
    {
        private Image icon;
        private TextMeshProUGUI count;
        private TextMeshProUGUI Level; //等级
        private TextMeshProUGUI powor; //强化
        private Button ActionBtn;
        private string currentID;
        public override void Init()
        {
            icon = GetComponent<Image>();
            count = Get<TextMeshProUGUI>("Count");
            ActionBtn = GetComponent<Button>();
            Level = Get<TextMeshProUGUI>("Level");
            powor = Get<TextMeshProUGUI>("Powor");
        }

        public void InitData(ItemBag bag)
        {
            Item item = InventoryManager.Instance.GetItem(bag.ID);
            count.text = bag.count.ToString();
            icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);
            Level.text = "lv: "+item.level;
            if (bag.power > 0)
            {
                powor.text = "+" + bag.power;
            }
            else
            {
                powor.text = "";
            }
            currentID = bag.ID;
            Bind(ActionBtn, delegate
            {
                 UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").ShowItemToolTip(bag);
            },UI_ToolAudio.UI_click.ToString());
        }
        
        public void InitData(ItemBag bag,Action func)
        {
            Item item = InventoryManager.Instance.GetItem(bag.ID);
            count.text = bag.count.ToString();
            icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);
            Level.text = "lv: "+item.level;
            if (bag.power > 0)
            {
                powor.text = "+" + bag.power;
            }
            else
            {
                powor.text = "";
            }
            currentID = bag.ID;
            Bind(ActionBtn,func,UI_ToolAudio.UI_click.ToString());
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
