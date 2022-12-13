using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG.UI
{
    /// <summary>
    /// 游戏主菜单UI
    /// </summary>
    public class MainPanel : Singleton<MainPanel>
    {
        private RootTableConfig Config;
        private RectTransform TabContent;
        private RootTabBtn BtnPrefab;
        private List<RootTabBtn> RootBtns;

        protected override void Awake()
        {
            base.Awake();
            TabContent = transform.Find("UIMask/DownUI/Content").GetComponent<RectTransform>();
            Config = ConfigManager.LoadConfig<RootTableConfig>("RootTable/RootTable");
            BtnPrefab = UISystem.Instance.GetPrefab<RootTabBtn>("RootBtnItem");
            CreatTbaleBtn();
        }

        /// <summary>
        /// 初始化加载DonwUI 下的Button
        /// </summary>
        private void CreatTbaleBtn()
        {
            foreach (var Btn in Config.tableItems)
            {
                RootTabBtn btn = Instantiate(BtnPrefab, TabContent);
                btn.Init();
                btn.InitData(Btn);
                RootBtns.Add(btn);
            }
        }


        /// <summary>
        /// 切换当前主状态
        /// </summary>
        /// <param name="type"></param>
        public void SwitchTabBtn(TableType type)
        {
            foreach (var btn in RootBtns)
            {
                btn.SetState(btn._type == type);
            }
            
            //TODO： 开启x秒Fade 过度
        }
    }

}
