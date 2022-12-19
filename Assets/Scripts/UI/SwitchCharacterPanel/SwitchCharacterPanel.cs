using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SwitchCharacterPanel : UIBase
    {
        private RectTransform content;
        private List<CharacterSlotUI> SlotUis;
        private Button CloseBtn;
        /// <summary>
        /// 选中的激活列表
        /// </summary>
        private RectTransform selectContent;
        /// <summary>
        /// 当前选中的总数
        /// </summary>
        private int currentCount;

        private SelectSlotUI[] SelectSlotUis;
        private TextMeshProUGUI currentText;
        private TextMeshProUGUI maxText;

        public override void Init()
        {
            content = Get<RectTransform>("UIMask/Back/Info/Scroll View/Content");
            SlotUis = new List<CharacterSlotUI>();
            CloseBtn = Get<Button>("UIMask/Back/CloseBtn");
            Bind(CloseBtn, Close, "OutChick");
            selectContent = Get<RectTransform>("UIMask/Back/Info/SelectPanel");
            SelectSlotUis = new SelectSlotUI[Settings.MaxSelectAmount];
            currentText = Get<TextMeshProUGUI>("UIMask/Back/Info/Row1/Image/CurrentAmount");
            maxText = Get<TextMeshProUGUI>("UIMask/Back/Info/Row1/Image/MaxAmount");
        }

        /// <summary>
        /// 创建出战角色UI
        /// </summary>
        public void CreateChacacterSlotUI()
        {
            UIHelper.Clear(content);
            UIHelper.Clear(selectContent);
            SlotUis.Clear();
            SelectSlotUis = new SelectSlotUI[Settings.MaxSelectAmount];
            currentCount = 0;
            currentText.text = "当前出战: <color=red>" + currentCount + "</color>";
            
            maxText.text = "当前出战总上限:<color=red>" + Settings.MaxSelectAmount + "</color>";
            List<CharacterBag> characterBags =  InventoryManager.Instance.GetBag();

            for (int i = 0; i < characterBags.Count; i++)
            {
                CharacterSlotUI Slot =  UISystem.Instance.InstanceUI<CharacterSlotUI>("CharacterSlotUI", content);
                Slot.InitData(i,characterBags[i]);
                SlotUis.Add(Slot);
            }
        }


        /// <summary>
        /// 刷新选中角色UI
        /// </summary>
        /// <param name="index">创建时的列表index</param>
        public void SetSelect(int index)
        {
            //1.首先判断是否超过最大边界值,如果超过,则该次点击无效
            if (currentCount >= Settings.MaxSelectAmount) return;
            currentCount++;
            currentText.text = "当前出战: <color=red>" + currentCount + "</color>";
            for (int i = 0; i < SlotUis.Count; i++)
            {
                SlotUis[i].SetSelect(false);
            }
            SlotUis[index].SetSelect(true);
            //2.在下面已激活队伍中生成对象
            CreateSelectHead(index);
        }

        /// <summary>
        /// 创建下方选中显示UI
        /// </summary>
        /// <param name="index">创建的index</param>
        public void CreateSelectHead(int index)
        {
            List<CharacterBag> characterBags =  InventoryManager.Instance.GetBag();
            SelectSlotUI Slot = UISystem.Instance.InstanceUI<SelectSlotUI>("SelectSlotUI", selectContent);
            Slot.InitData(currentCount-1 ,characterBags[index]);
            SelectSlotUis[currentCount - 1] = Slot;
        }

        /// <summary>
        /// 刷新创建下方选中显示UI
        /// </summary>
        /// <param name="index"></param>
        /// <param name="DestoryObj"></param>
        public void UpdateSlectHead(int index,SelectSlotUI DestoryObj)
        {
            SelectSlotUis[index] = null;
            currentCount--;
            currentText.text = "当前出战: <color=red>" + currentCount + "</color>";
            for (int i = 0; i < SlotUis.Count; i++)
            {
                if (SlotUis[i].currentdata.ID != DestoryObj.currentdata.ID) continue;
                SlotUis[i].SetSelect(false);
                break;
            }
            
            
            if(DestoryObj != null)
                Destroy(DestoryObj.gameObject);
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("SwitchCharacterPanel");
        }
    }
}

