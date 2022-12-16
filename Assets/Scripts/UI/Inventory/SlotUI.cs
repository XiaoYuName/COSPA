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
        private ItemBag currentItme;
        private Image icon;
        private TextMeshProUGUI count;
        private Button ActionBtn;
        public override void Init()
        {
            icon = GetComponent<Image>();
            count = Get<TextMeshProUGUI>("Count");
            ActionBtn = GetComponent<Button>();
        }

        public void InitData(ItemBag bag)
        {
            currentItme = bag;
            Item item = InventoryManager.Instance.GetItem(bag.ID);
            count.text = bag.count.ToString();
            Bind(ActionBtn, delegate
            {
                 UISystem.Instance.GetUI<CharacterEquipPanel>("CharacterEquipPanel").ShowItemToolTip(bag);
            },"PopWindows" );
        }
    }

}
