using System;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SetpUI : UIBase
    {
        private TextMeshProUGUI ItemName;
        private MaterialSlotUI SlotUI;
        private Slider Slider;
        private TextMeshProUGUI SliderValue;
        private TextMeshProUGUI ExpendGold;
        private TextMeshProUGUI Gold;
        private Button StepBtn;
        private CharacterBag currentBag;
        
        public override void Init()
        {
            ItemName = Get<TextMeshProUGUI>("ItemName");
            SlotUI = Get<MaterialSlotUI>("MaterialSlotUI");
            Slider = Get<Slider>("Slider");
            SliderValue = Get<TextMeshProUGUI>("Slider/SliderValue");
            ExpendGold = Get<TextMeshProUGUI>("ExpendGold/Value");
            Gold = Get<TextMeshProUGUI>("Gold/Value");
            StepBtn = Get<Button>("StepBtn");
            SlotUI.Init();
            MessageAction.UpCharacterBag += MessageActionOnUpCharacterBag;
        }

        private void OnDestroy()
        {
            MessageAction.UpCharacterBag -= MessageActionOnUpCharacterBag;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="info"></param>
        public void InitData(CharacterConfigInfo info)
        {
            currentBag = InventoryManager.Instance.GetBag(info.ID);
            if (currentBag.currentStar >= info.StepData.Length)
            {
                Debug.Log("已经满级");
                return;
            }

            StepInI data = info.StepData[currentBag.currentStar -1];
            Item item = InventoryManager.Instance.GetItem(data.ItemID);
            ItemName.text = item.ItemName;
            SlotUI.InitData(item);
            
            ItemBag itemBag = InventoryManager.Instance.GetItemBag(item.ID);
            
            Slider.minValue = 0;
            Slider.maxValue = data.Amount;
            Slider.value = itemBag?.count ?? 0;
            if (itemBag == null ||itemBag.count == 0)
            {
                SliderValue.text = 0 + "/" + Slider.maxValue;
            }
            else
            {
                SliderValue.text = itemBag.count + "/" + Slider.maxValue;
            }
            ExpendGold.text = data.Gold.ToString();
            Gold.text = InventoryManager.Instance.GetItemBag(Settings.ManaID).count.ToString();
            Bind(StepBtn, delegate
            {
                if (itemBag == null)
                {
                    UISystem.Instance.ShowPopWindows("提示","背包没有该材料","确定");
                    return;
                }
                if (itemBag.count < data.Amount)
                {
                    UISystem.Instance.ShowPopWindows("提示","材料不满足要求","确定");
                    return;
                }

                ItemBag maNaBag = InventoryManager.Instance.GetItemBag(Settings.ManaID);
                if (maNaBag.count < data.Gold)
                {
                    UISystem.Instance.ShowPopWindows("提示","玛那不足","确定");
                    return;
                }

                currentBag.currentStar++;
                InventoryManager.Instance.DeleteItemBag(maNaBag,data.Gold);
                InventoryManager.Instance.DeleteItemBag(itemBag,data.Amount);
                MessageAction.OnUpCharacterBag(currentBag);
                UISystem.Instance.ShowPopWindows("提示","觉醒成功","确定");
            }, "UI_click");
        }
        
        //刷新数据
        private void MessageActionOnUpCharacterBag(CharacterBag characterBag)
        {
            if (currentBag.ID == characterBag.ID)
            {
                InitData(InventoryManager.Instance.GetCharacter(characterBag.ID));
            }
        }
    }
}

