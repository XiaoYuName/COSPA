using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 宝石商店面板
    /// </summary>
    public class StorePanel : UIBase
    {
        private Button CloseBtn;
        private Button GemsthoneBtn;
        private Button MaNaBtn;
        public override void Init()
        {
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OutChick");
            GemsthoneBtn = Get<Button>("UIMask/SwitchBtns/GemsthoneBtn");
            MaNaBtn = Get<Button>("UIMask/SwitchBtns/ManaBtn");
            Bind(GemsthoneBtn,()=>OnChick(StoreType.宝石),"OnChick");
            Bind(MaNaBtn,()=>OnChick(StoreType.玛娜),"OnChick");
        }



        private void OnChick(StoreType type)
        {
            void Func(StorePopWindows pop)
            {
                pop.SwitchCreatStoreItemUI(type);
            }

            UISystem.Instance.OpenUI<StorePopWindows>("StorePopWindows", Func);
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("StorePanel");
        }
    }
}

