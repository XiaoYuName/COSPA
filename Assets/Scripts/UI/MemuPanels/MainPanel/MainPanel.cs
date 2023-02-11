using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using NaughtyAttributes;
using UnityEngine;

namespace ARPG.UI
{
    /// <summary>
    /// 游戏主菜单UI
    /// </summary>
    public class MainPanel : MonoSingleton<MainPanel>
    {
        private RootTableConfig Config;
        private Transform TabContent;
        private RootTabBtn[] RootBtns;
        private TableType currentType; //当前Type
        private List<string> ChildUITabel = new List<string>(); //二级菜单子界面,当打开时,注册列表中,当点击菜单内的按钮时,来控制一键关闭所有子菜单

        protected override void Awake()
        {
            base.Awake();
            TabContent = transform.Find("UIMask/Down");
            Config = ConfigManager.LoadConfig<RootTableConfig>("RootTable/RootTable");
            CreatTbaleBtn();
            SwitchTabBtn(TableType.我的主页);
            currentType = TableType.我的主页;
            UISystem.Instance.OpenUI("HomeScene");
            WaitUtils.WaitTimeDo(1.5f, () => UISystem.Instance.ShowPopNotice());
        }

        /// <summary>
        /// 初始化加载DonwUI 下的Button
        /// </summary>
        private void CreatTbaleBtn()
        {
            RootBtns = TabContent.transform.GetComponentsInChildren<RootTabBtn>();
            foreach (var Tbtn in RootBtns)
            {
                Tbtn.Init();
            }
        }


        /// <summary>
        /// 切换当前主状态
        /// </summary>
        /// <param name="type"></param>
        public void SwitchTabBtn(TableType type)
        {
            if (ChildUITabel.Count >= 1) //关闭所有子项菜单
            {
                //这里需要逆序遍历
                for (int i = ChildUITabel.Count -1; i >= 0; i--)
                {
                    UISystem.Instance.CloseUI(ChildUITabel[i]);
                }
            }
            
            string UIname = Config.GetOpenName(currentType);
            SetRootBtnAnim(currentType, false);
            UISystem.Instance.CloseUI(UIname);
            currentType = type;
            SetRootBtnAnim(currentType, true);
        }

        
        public RootTableItem GetTabeleData(TableType type)
        {
            return Config.Get(type);
        }

        private void SetRootBtnAnim(TableType type,bool isPlay)
        {
            foreach (var rootBtn in RootBtns)
            {
                if (rootBtn._type == type)
                {
                    rootBtn.PlayAnimation(isPlay);
                }
            }
        }

        public void AddTbaleChild(string table)
        {
            ChildUITabel.Add(table);
        }

        public void RemoveTableChild(string table)
        {
            if(ChildUITabel.Contains(table))
                ChildUITabel.Remove(table);
        }

        public void Close()
        {
            if (ChildUITabel.Count >= 1) //关闭所有子项菜单
            {
                //这里需要逆序遍历
                for (int i = ChildUITabel.Count -1; i >= 0; i--)
                {
                    UISystem.Instance.CloseUI(ChildUITabel[i]);
                }
            }
            
            //关闭当前激活菜单
            string UIname = Config.GetOpenName(currentType);
            UISystem.Instance.CloseUI(UIname);
            
        }
    }

}
