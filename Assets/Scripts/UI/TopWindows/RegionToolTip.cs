using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class RegionToolTip : UIBase
    {
        private Button CloseBtn;
        private Button OpenBtn;
        private RectTransform ItemContent;
        private RectTransform RewordContent;
        private RectTransform EnemyContent;
        
        private TextMeshProUGUI MapName;
        private Image BackImage;
        private MapConfig MapConfig;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Back/RightPanel/CloseBtn");
            Bind(CloseBtn,Close,UI_ToolAudio.OutChick.ToString());
            ItemContent = Get<RectTransform>("UIMask/Back/LeftPanel/ItemRow/Content");
            RewordContent = Get<RectTransform>("UIMask/Back/LeftPanel/RewordRow/Content");
            EnemyContent = Get<RectTransform>("UIMask/Back/LeftPanel/EnemyRow/Panel");
            BackImage = Get<Image>("UIMask/Back/Mask/Back");
            MapName = Get<TextMeshProUGUI>("UIMask/Back/Mask/Back/Farme/MapName");
            MapConfig = ConfigManager.LoadConfig<MapConfig>("Map/MapData");
            OpenBtn = Get<Button>("UIMask/Back/RightPanel/OpenBtn");
        }

        public void InitData(RegionItem regionItem)
        {
            MapItem mapItem = MapConfig.Get(regionItem.RegionItemName);
            MapName.text = mapItem.ID;
            BackImage.sprite = mapItem.mapIcon;
            CreateSlotUI(mapItem.RewordItemList);
            UIHelper.Clear(EnemyContent);
            for (int i = 0; i < regionItem.WaveItems.Count; i++)
            {
                CreateEnemyUI(i,regionItem.WaveItems[i].EnemyList);
            }
            
            Bind(OpenBtn, delegate
            {
                void Func(SwitchCharacterPanel ui)
                {
                    ui.CreateChacacterSlotUI(regionItem);
                    MainPanel.Instance.AddTbaleChild("SwitchCharacterPanel");
                }
                UISystem.Instance.OpenUI<SwitchCharacterPanel>("SwitchCharacterPanel", Func);
            }, "OnChick");
        }

        private void CreateSlotUI(List<ItemBag> Reword)
        {
            UIHelper.Clear(ItemContent);
            foreach (var Item in Reword)
            {
                SlotUI Slot = UISystem.Instance.InstanceUI<SlotUI>("SlotUI",ItemContent);
                Slot.InitData(Item,delegate {  });
            }
        }


        private void CreateEnemyUI(int win,List<EnemyBag> Enemys)
        {
            foreach (var Item in Enemys)
            {
                EnemySlotUI Slot = UISystem.Instance.InstanceUI<EnemySlotUI>("EnemySlotUI",EnemyContent);
                Slot.InitData(win,Item);
            }
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("RegionToolTip");
        }
    }

}
