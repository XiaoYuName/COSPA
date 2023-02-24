using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 主菜单界面
    /// </summary>
    public class SettingsPanel : UIBase
    {
        private Button SaveBtn;
        private MoneyUI MoneyUI;
        private Button ReturnHomeBtn;
        private Button StoreBtn;
        private Button InventoryBtn;
        private Button SettingBtn;
        public override void Init()
        {
            SaveBtn = Get<Button>("UIMask/Content/SaveUser");
            MoneyUI = Get<MoneyUI>("UIMask/MoneyPoint/MoneyUI");
            ReturnHomeBtn = Get<Button>("UIMask/Content/ReturnHome");
            InventoryBtn = Get<Button>("UIMask/Content/ItemBag");
            StoreBtn = Get<Button>("UIMask/Content/GemsthoneShop");
            SettingBtn = Get<Button>("UIMask/Content/Setting");
            MoneyUI.Init();
            Bind(SaveBtn,()=>InventoryManager.Instance.SaveUserData(),"UI_click");
            Bind(ReturnHomeBtn,ReturnHome,"UI_click");
            Bind(InventoryBtn, delegate
            {
                FadeManager.Instance.PlayFade(0.2f, delegate
                {
                    UISystem.Instance.OpenUI("InventoryUI");
                    MainPanel.Instance.AddTbaleChild("InventoryUI");
                },0f);

            }, "UI_click");
            
            Bind(StoreBtn, delegate
            {
                FadeManager.Instance.PlayFade(0.2f, delegate
                {
                    UISystem.Instance.OpenUI("StorePanel");
                    MainPanel.Instance.AddTbaleChild("StorePanel");
                },0f);
            },"UI_click");
            
            Bind(SettingBtn, delegate
            {
                UISystem.Instance.ShowPopSetting();
            }, UiAudioID.UI_click);
        }

        private void ReturnHome()
        {
            UISystem.Instance.ShowPopDialogue("返回","确定要返回主菜单吗?,请确保保存了数据,否则数据将丢失","确定",
                "返回",CloseOpenUI,null);
        }

        private void CloseOpenUI()
        {
            Close();
            TaskManager.Instance.StopTimer();
            MessageAction.OnTransitionEvent("LoadingScene",Vector3.zero);
        }
    }
}

