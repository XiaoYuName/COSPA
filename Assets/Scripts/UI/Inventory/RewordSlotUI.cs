using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 奖励Reword 可以显示装备和材料道具
    /// </summary>
    public class RewordSlotUI : UIBase
    {
        private Image icon;
        private TextMeshProUGUI count;
        private TextMeshProUGUI Level;
        private TextMeshProUGUI Powor;
        private GameObject Type;
        public override void Init()
        {
            icon = GetComponent<Image>();
            count = Get<TextMeshProUGUI>("Count");
            Level = Get<TextMeshProUGUI>("Level");
            Powor = Get<TextMeshProUGUI>("Powor");
            Type = Get("Type");
        }

        public void InitData(RewordItemBag RewordItem)
        {
            Item item = InventoryManager.Instance.GetItem(RewordItem.itemBag.ID);
            if (item.Type == ItemType.材料)
            {
                ShowMaterial(RewordItem,item);
            }
            else
            {
                ShowEquip(RewordItem,item);
            }

            SetType(RewordItem.Type);
        }

        public void ShowEquip(RewordItemBag RewordItem,Item item)
        {
            icon.sprite = item.icon;
            count.text = RewordItem.itemBag.count.ToString();
            
            Level.gameObject.SetActive(true);
            Level.text = "le: "+item.level;
            Powor.gameObject.SetActive(true);
            Powor.text = "+" + RewordItem.itemBag.power;
        }

        public void ShowMaterial(RewordItemBag RewordItem,Item item)
        {
            icon.sprite = item.icon;
            count.text = RewordItem.itemBag.count.ToString();
            Level.gameObject.SetActive(false);
            Powor.gameObject.SetActive(false);
        }


        public void SetType(RewordType type)
        {
            Type.gameObject.SetActive(type != RewordType.Not);
            for (int i = 0; i < Type.transform.childCount; i++)
            {
                Type.transform.GetChild(i).gameObject.SetActive(Type.transform.GetChild(i).name == type.ToString());
            }
        }
    }
}

