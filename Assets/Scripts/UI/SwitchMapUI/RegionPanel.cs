using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.UI.Config;

namespace ARPG.UI
{
    public class RegionPanel : UIBase
    {
         /// <summary>
        /// 副本列表
        /// </summary>
        private RectTransform RegionContent;
        private RectTransform RegionChildContent;
        private RegionTableUI RegionItemUI;
        private RegionConfig MainConfig;
        private Animator anim;
        private RegionTableUI OpenTableUI;
        
        /// <summary>
        /// 副本进度 Vector2(主线x -  章节x)
        /// </summary>
        private Vector2Int PrincProgress;

        /// <summary>
        /// 副本
        /// </summary>
        private List<RegionLine> MainPrincList = new List<RegionLine>();

        private static readonly int s_ShowView = Animator.StringToHash("ShowView");

        public override void Init()
        {
            PrincProgress = InventoryManager.Instance.GetPrincPress();
            MainConfig = ConfigManager.LoadConfig<RegionConfig>("Region/Region");
            RegionItemUI = UISystem.Instance.GetPrefab<RegionTableUI>("RegionTableUI");
            RegionContent = Get<RectTransform>("UIMask/RegionLineView/View/Content");
            RegionChildContent = Get<RectTransform>("UIMask/RegionItemView/View/Content");
            MainPrincList = MainConfig.RegionList;
            anim = GetComponent<Animator>();
            CreateLineItemUI();
            MessageAction.RefRegionPress += SetUpdateRegionPress;
        }

        /// <summary>
        /// 刷新主线进度
        /// </summary>
        public void SetUpdateRegionPress()
        {
            PrincProgress = InventoryManager.Instance.GetPrincPress();
            CreateLineItemUI();
        }


        /// <summary>
        /// 生成章节
        /// </summary>
        public void CreateLineItemUI()
        {
            UIHelper.Clear(RegionContent);
            for (int i = 0; i < MainPrincList.Count; i++)
            {
                RegionTableUI ItemUI = Instantiate(RegionItemUI, RegionContent);
                ItemUI.Init();
                if (i < PrincProgress.x)
                {
                    ItemUI.InitData(1,MainPrincList[i],i);
                }else if (i == PrincProgress.x )
                {
                    ItemUI.InitData(2,MainPrincList[i],i);
                }
                else
                {
                    ItemUI.InitData(3,MainPrincList[i],i);
                }
            }
        }

        /// <summary>
        /// 生成章节子剧情
        /// </summary>
        public void CreateLineItemChlidUI(int index)
        {
            UIHelper.Clear(RegionChildContent);
            for (int i = 0; i < MainPrincList[index].RegionItemList.Count; i++)
            {
                RegionTableUI ItemUI = Instantiate(RegionItemUI, RegionChildContent);
                ItemUI.Init();
                if (i < PrincProgress.y)
                {
                    ItemUI.InitData(i,MainPrincList[index].RegionItemList[i],1);
                }else if (i == PrincProgress.y)
                {
                    ItemUI.InitData(i,MainPrincList[index].RegionItemList[i],2);
                }
                else
                {
                    ItemUI.InitData(i,MainPrincList[index].RegionItemList[i],3);
                }
            }
        }

        public void SetAnimator(bool isShow)
        {
            anim.SetBool(s_ShowView,isShow);
        }

        public void SetCurrentOpenTable(RegionTableUI tableUI)
        {
            OpenTableUI = tableUI;
        }

        public override void Close()
        {
            SetAnimator(false);
            if(OpenTableUI != null)
             OpenTableUI.isClick = false;
            base.Close();
            MainPanel.Instance.RemoveTableChild("RegionPanel");
        }

        public void OnDestroy()
        {
            MessageAction.RefRegionPress -= SetUpdateRegionPress;
        }
    }
}

