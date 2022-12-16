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
        private PrincLineItemUI _lineItemUI;
        private PrincipalLineConfig MainConfig;

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
            MainPrincList = MainConfig.MianPrincipalLineList;
            CreateLineItemUI();

        }


        public void CreateLineItemUI()
        {
            UIHelper.Clear(PrincContent);
            for (int i = 0; i < MainPrincList.Count; i++)
            {
                PrincLineItemUI ItemUI = Instantiate(_lineItemUI, PrincContent);
                ItemUI.Init();
                if (i < PrincProgress.x)
                {
                    ItemUI.InitData(1,MainPrincList[i]);
                }else if (i == PrincProgress.x )
                {
                    ItemUI.InitData(2,MainPrincList[i]);
                }
                else
                {
                    ItemUI.InitData(3,MainPrincList[i]);
                }
            }
        }
    }
}

