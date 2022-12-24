using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG.UI
{
    public class SwicthMapPanel : UIBase
    {
        /// <summary>
        /// 主线列表
        /// </summary>
        private RectTransform PrincContent;
        private RectTransform PrincChildContent;
        private PrincLineItemUI _lineItemUI;
        private PrincipalLineConfig MainConfig;
        private Animator anim;

        /// <summary>
        /// 主线剧情进度 Vector2(主线x -  章节x)
        /// </summary>
        private Vector2Int PrincProgress;

        /// <summary>
        /// 主线
        /// </summary>
        private List<PrincLine> MainPrincList = new List<PrincLine>();

        public override void Init()
        {
            PrincProgress = Vector2Int.zero;
            MainConfig = ConfigManager.LoadConfig<PrincipalLineConfig>("PrincipalLineConfig/MainPrinc");
            _lineItemUI = UISystem.Instance.GetPrefab<PrincLineItemUI>("PrincLineItemUI");
            PrincContent = Get<RectTransform>("UIMask/PrincipalLineView/View/Content");
            PrincChildContent = Get<RectTransform>("UIMask/PrincipaItemView/View/Content");
            MainPrincList = MainConfig.MianPrincipalLineList;
            anim = GetComponent<Animator>();
            CreateLineItemUI();

        }
        
        /// <summary>
        /// 生成章节
        /// </summary>
        public void CreateLineItemUI()
        {
            UIHelper.Clear(PrincContent);
            for (int i = 0; i < MainPrincList.Count; i++)
            {
                PrincLineItemUI ItemUI = Instantiate(_lineItemUI, PrincContent);
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
            UIHelper.Clear(PrincChildContent);
            for (int i = 0; i < MainPrincList[index].PrincItemList.Count; i++)
            {
                PrincLineItemUI ItemUI = Instantiate(_lineItemUI, PrincChildContent);
                ItemUI.Init();
                if (i < PrincProgress.y)
                {
                    ItemUI.InitData(i,MainPrincList[index].PrincItemList[i],1);
                }else if (i == PrincProgress.y)
                {
                    ItemUI.InitData(i,MainPrincList[index].PrincItemList[i],2);
                }
                else
                {
                    ItemUI.InitData(i,MainPrincList[index].PrincItemList[i],3);
                }
            }
        }

        public void SetAnimator(bool isShow)
        {
            anim.SetBool("ShowView",isShow);
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("SwicthMapPanel");
        }
    }
}

