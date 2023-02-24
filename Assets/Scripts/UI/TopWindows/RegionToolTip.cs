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
            Bind(CloseBtn,Close,UiAudioID.OutChick);
            ItemContent = Get<RectTransform>("UIMask/Back/LeftPanel/ItemRow/Content");
            RewordContent = Get<RectTransform>("UIMask/Back/LeftPanel/RewordRow/Content");
            
            EnemyContent = Get<RectTransform>("UIMask/Back/LeftPanel/EnemyRow/EnemyView/Viewport/Content");
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
            UIHelper.Clear(RewordContent);
            CreateMontySlotUI(mapItem.MoneyReword);
            
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

        /// <summary>
        /// 生成Item信息
        /// </summary>
        /// <param name="Reword"></param>
        private void CreateSlotUI(List<RewordItemBag> Reword)
        {
            UIHelper.Clear(ItemContent);
            foreach (var Item in Reword)
            {
                MaterialSlotUI Slot = UISystem.Instance.InstanceUI<MaterialSlotUI>("MaterialSlotUI",ItemContent);
                Slot.InitData(Item.itemBag);
            }
        }
        
        /// <summary>
        /// 生成怪物信息
        /// </summary>
        /// <param name="win">波数</param>
        /// <param name="Enemys">怪物</param>
        private void CreateEnemyUI(int win,List<EnemyBag> Enemys)
        {
            foreach (var Item in Enemys)
            {
                EnemySlotUI Slot = UISystem.Instance.InstanceUI<EnemySlotUI>("EnemySlotUI",EnemyContent);
                Slot.InitData(win,Item);
            }
        }

        /// <summary>
        /// 生成货币奖励详情
        /// </summary>
        /// <param name="Reword"></param>
        private void CreateMontySlotUI( RewordItemBag[] Reword)
        {
            foreach (var Item in Reword)
            {
                MaterialSlotUI Slot = UISystem.Instance.InstanceUI<MaterialSlotUI>("MaterialSlotUI",RewordContent);
                Slot.InitData(Item.itemBag);
            }
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("RegionToolTip");
        }
    }

}
