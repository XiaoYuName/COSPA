using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
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
            icon.sprite = item.icon;
            Level.text = "lv: "+item.level;
            powor.text = "+" + bag.power;
            Bind(ActionBtn, delegate
            {
                 UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").ShowItemToolTip(bag);
            },"PopWindows" );
        }
    }

}
