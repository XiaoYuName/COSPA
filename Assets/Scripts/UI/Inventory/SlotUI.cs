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
    public class SlotUI : UIBase
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
            },UiAudioID.UI_click);
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
            Bind(ActionBtn,func,UiAudioID.UI_click);
        }
        
    }

}
