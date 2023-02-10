using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace  ARPG.UI
{
    public class PopShowItem : UIBase
    {
        private Image icon;
        private TextMeshProUGUI ItemName;
        private TextMeshProUGUI ItemAmount;
        private TextMeshProUGUI description;
        private Button CloseBtn;
        private bool isShow;
        public override void Init()
        {
            icon = Get<Image>("Back/Farme/info/Farme/icon");
            ItemName = Get<TextMeshProUGUI>("Back/Farme/info/ItemName");
            ItemAmount = Get<TextMeshProUGUI>("Back/Farme/info/PropValue/Value");
            description = Get<TextMeshProUGUI>("Back/Farme/info/description/description");
            CloseBtn = Get<Button>("Back/CloseBtn");
            Bind(CloseBtn,CloseShow,"OutChick");
            transform.localScale = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="idType">ID类型</param>
        /// <param name="ID">ID</param>
        public void Show(IDType idType,string ID)
        {
            if (isShow) return;
            isShow = true;
            Open();
            switch (idType)
            {
                case IDType.怪物:
                     EnemyData config = EnemyManager.Instance.config.Get(ID);
                     ItemName.text = config.EnemyName;
                     icon.sprite = config.icon;
                     ItemAmount.text = "不可持有";
                     description.text = config.description;
                    break;
                case IDType.物品:
                    Item item = InventoryManager.Instance.GetItem(ID);
                    if (item != null)
                    {
                        ItemName.text = item.ItemName;
                        icon.sprite = GameSystem.Instance.GetSprite(item.spriteID);
                        ItemBag bag = InventoryManager.Instance.GetItemBag(ID);
                        if (bag == null)
                        {
                            ItemAmount.text = "0";
                        }
                        else
                        {
                            ItemAmount.text = "<color=red>"+bag.count + "</color>";
                        }
                        description.text = item.description;
                    }
                    break;
                case IDType.角色:
                   CharacterConfigInfo info =  InventoryManager.Instance.GetCharacter(ID);
                   if (info != null)
                   {
                       ItemName.text = info.CharacterName;
                       icon.sprite = info.Headicon;
                      CharacterBag Bag =  InventoryManager.Instance.GetCharacterBag(ID);
                      ItemAmount.text = Bag == null ? "暂未拥有" : "已拥有";
                      description.text = info.des;
                   }
                   break;
            }
            transform.DOScale(new Vector3(1, 1, 1), Settings.isShowItemTime).OnComplete(()=>isShow=false);
        }

        /// <summary>
        /// 关闭界面显示
        /// </summary>
        public void CloseShow()
        {
            transform.DOScale(new Vector3(0, 0, 0), Settings.isShowItemTime).OnComplete(() =>
            {
                isShow = false;
                Close();
            });
        }
    }
}

