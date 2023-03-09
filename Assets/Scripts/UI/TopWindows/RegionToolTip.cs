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
            ItemContent = Get<RectTransform>("UIMask/Back/LeftPanel/ItemRow/RewordView/Viewport/Content");
            RewordContent = Get<RectTransform>("UIMask/Back/LeftPanel/RewordRow/Content");
            
            EnemyContent = Get<RectTransform>("UIMask/Back/LeftPanel/EnemyRow/EnemyView/Viewport/Content");
            BackImage = Get<Image>("UIMask/Back/Mask/Back");
            MapName = Get<TextMeshProUGUI>("UIMask/Back/Mask/Back/Farme/MapName");
            MapConfig = ConfigManager.LoadConfig<MapConfig>("Map/MapData");
            OpenBtn = Get<Button>("UIMask/Back/RightPanel/OpenBtn");
        }

        /// <summary>
        /// 初始化显示主线副本
        /// </summary>
        /// <param name="line">主线配置</param>
        /// <param name="regionItem">章节配置</param>
        public void InitData(RegionLine line,RegionItem regionItem)
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
                    ui.CreateChacacterSlotUI(line,regionItem);
                    MainPanel.Instance.AddTbaleChild("SwitchCharacterPanel");
                }
                UISystem.Instance.OpenUI<SwitchCharacterPanel>("SwitchCharacterPanel", Func);
            }, "OnChick");
        }

        /// <summary>
        /// 初始化显示独立副本/随机类副本
        /// </summary>
        /// <param name="regionItem">副本</param>
        public void InitData(RegionItem regionItem)
        {
            //1.如果是随机副本,走随机副本流程,如果是独立副本,走独立副本流程
            if (Settings.isRandomRegion(regionItem.RegionItemName))
            {
                RandomMapItem randomMapItem = MapConfig.GetRandomMapItem(regionItem.RegionItemName);
                MapName.text = randomMapItem.ID;
                BackImage.sprite = randomMapItem.mapIcon;
                CreateSlotUI(randomMapItem.RewordItemList);
                UIHelper.Clear(EnemyContent);
                for (int i = 0; i < regionItem.WaveItems.Count; i++)
                {
                    CreateEnemyUI(i,regionItem.WaveItems[i].EnemyList);
                }
                UIHelper.Clear(RewordContent);
                CreateMontySlotUI(randomMapItem.MoneyReword);
            
                Bind(OpenBtn, delegate
                {
                    void Func(SwitchCharacterPanel ui)
                    {
                        ui.CreateChacacterSlotUI(regionItem);
                        MainPanel.Instance.AddTbaleChild("SwitchCharacterPanel");
                    }
                    UISystem.Instance.OpenUI<SwitchCharacterPanel>("SwitchCharacterPanel", Func);
                }, "OnChick");
                return;
            }
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
                    ui.CreateChacacterSlotUI(null,regionItem);
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
                RewordSlotUI rewordSlotUI = UISystem.Instance.InstanceUI<RewordSlotUI>("RewordSlotUI", ItemContent);
                rewordSlotUI.InitData(new RewordItemBag()
                {
                    itemBag = Item.itemBag,
                    Type = Item.Type,
                });
            }
        }
        
        private void CreateSlotUI(List<RandomRewordItemBag> Reword)
        {
            UIHelper.Clear(ItemContent);
            foreach (var Item in Reword)
            {
                RewordSlotUI rewordSlotUI = UISystem.Instance.InstanceUI<RewordSlotUI>("RewordSlotUI", ItemContent);
                rewordSlotUI.InitData(new RewordItemBag()
                {
                    itemBag = Item.itemBag,
                    Type = Item.Type,
                });
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
                RewordSlotUI rewordSlotUI = UISystem.Instance.InstanceUI<RewordSlotUI>("RewordSlotUI", RewordContent);
                rewordSlotUI.InitData(new RewordItemBag()
                {
                    itemBag = Item.itemBag,
                    Type = Item.Type,
                });
            }
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("RegionToolTip");
        }
    }

}
