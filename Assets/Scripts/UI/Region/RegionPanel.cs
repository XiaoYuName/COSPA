using System.Collections.Generic;
using UnityEngine;
using ARPG.UI.Config;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class RegionPanel : UIBase
    {
         /// <summary>
        /// 副本列表
        /// </summary>
        private RectTransform RegionContent;
        private RegionConfig MainConfig;
        private Button CloseBtn;
        
        /// <summary>
        /// 副本进度 Vector2(主线x -  章节x)
        /// </summary>
        private Vector2Int PrincProgress;

        private List<RegionLineUI> LineList = new List<RegionLineUI>();
 
        public override void Init()
        {
            PrincProgress = InventoryManager.Instance.GetPrincPress();
            MainConfig = ConfigManager.LoadConfig<RegionConfig>("Region/Region");
            RegionContent = Get<RectTransform>("UIMask/MainView/RegionView/Viewport/Content");
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,UiAudioID.OutChick);
            CreateLineItemUI();
            MessageAction.RefRegionPress += SetUpdateRegionPress;
            SetUpdateRegionPress();
        }

        /// <summary>
        /// 刷新主线进度
        /// </summary>
        public void SetUpdateRegionPress()
        {
            PrincProgress = InventoryManager.Instance.GetPrincPress();
            for (int i = 0; i < LineList.Count; i++)
            {
                if (i < PrincProgress.x)
                {
                    LineList[i].SetProgress(LookState.已通关);
                }else if(i == PrincProgress.x)
                {
                    LineList[i].SetProgress(LookState.已解锁);
                }
                else
                {
                    LineList[i].SetProgress(LookState.未开启);
                }
            }
        }


        /// <summary>
        /// 生成章节
        /// </summary>
        public void CreateLineItemUI()
        {
            LineList.Clear();
            UIHelper.Clear(RegionContent);
            for (int i = 0; i < MainConfig.RegionList.Count; i++)
            {
                RegionLineUI lineUI = UISystem.Instance.InstanceUI<RegionLineUI>("RegionLineUI",RegionContent);
                lineUI.InitData(MainConfig.RegionList[i]);
                LineList.Add(lineUI);
            }
        }
        
        

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("RegionPanel");
        }

        public void OnDestroy()
        {
            MessageAction.RefRegionPress -= SetUpdateRegionPress;
        }
    }
}

