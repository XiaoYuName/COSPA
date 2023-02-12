using ARPG.Config;
using TMPro;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 玩家货币显示UI
    /// </summary>
    public class MoneyUI : UIBase
    {
        private TextMeshProUGUI GemsthoneText;
        private TextMeshProUGUI ManaText;
        private Button AddStGemsthoneBtn;
        private Button AddManaBtn;

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            MessageAction.UpdataeMoney+= SetMoneyUI;
            InventoryManager.Instance.UpdateMoney();
        }

        private void OnDestroy()
        {
            MessageAction.UpdataeMoney -= SetMoneyUI;
        }

        public override void Init()
        {
            GemsthoneText = Get<TextMeshProUGUI>("Content/Gemsthone/Amount");
            ManaText = Get<TextMeshProUGUI>("Content/Mana/Amount");
            AddStGemsthoneBtn = Get<Button>("Content/Gemsthone/AddBtn");
            AddManaBtn = Get<Button>("Content/Mana/AddBtn");
            
            //初始阶段手动刷新一次
            SetMoneyUI(InventoryManager.Instance.GetItemBag(Settings.GemsthoneID),
                InventoryManager.Instance.GetItemBag(Settings.ManaID));
            Bind(AddStGemsthoneBtn,()=>OnChick(StoreType.宝石), "UI_click");
            Bind(AddManaBtn,()=>OnChick(StoreType.玛娜), "UI_click");
        }
        
        /// <summary>
        /// 设置/刷新货币
        /// </summary>
        /// <param name="Gemsthone"></param>
        /// <param name="Mana"></param>
        public void SetMoneyUI(ItemBag Gemsthone, ItemBag Mana)
        {
            GemsthoneText.text = Gemsthone.count.ToString();
            ManaText.text = Mana.count.ToString();
        }
        
        
        private void OnChick(StoreType type)
        {
            void Func(StorePopWindows pop)
            {
                pop.SwitchCreatStoreItemUI(type);
            }

            UISystem.Instance.OpenUI<StorePopWindows>("StorePopWindows", Func);
        }
    }
}

