using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ARPG.UI.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class EquipStoenPanel : UIBase
    {
        private SwitchTableUIType[] TableBtns;
        private SwitchTableUIContent[] TableContent;
        private EquipStoenConfig _config;
        private Button CloseBtn;

        public override void Init()
        {
            _config = ConfigManager.LoadConfig<EquipStoenConfig>("Store/EquipStoen");
            TableBtns = GetComponentsInChildren<SwitchTableUIType>();
            TableContent = GetComponentsInChildren<SwitchTableUIContent>();
            for (int i = 0; i < TableBtns.Length; i++)
            {
                TableBtns[i].Init();
                TableBtns[i].BindOnClick(SwitchTableUI);
            }

            for (int i = 0; i < TableContent.Length; i++)
            {
                TableContent[i].Init();
            }

            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,UiAudioID.OutChick);

            CreatTabEquipStoenItemUI();
            SwitchTableUI(EquipTableType.通常);
        }

        private void SwitchTableUI(EquipTableType _type)
        {
            if (TableBtns.Any(t => t.tableType == _type) && TableContent.Any(t => t.tableType == _type))
            {
                for (int i = 0; i < TableBtns.Length; i++)
                {
                    TableBtns[i].OnClick(TableBtns[i].tableType == _type);
                }

                for (int i = 0; i < TableContent.Length; i++)
                {
                    TableContent[i].gameObject.SetActive(TableContent[i].tableType == _type);
                }
            }
        }


        private void CreatTabEquipStoenItemUI()
        {
            for (int i = 0; i < TableBtns.Length; i++)
            {
               List<EquipStoenData> TypeList = _config.GetTypeList(TableBtns[i].tableType);
               if(TypeList == null)continue;
               UIHelper.Clear(TableContent[i].GetContent());
               for (int j = 0; j < TypeList.Count; j++)
               {
                   EquipStoenItemUI SlotUI =  UISystem.Instance.InstanceUI<EquipStoenItemUI>("EquipStoenItemUI", TableContent[i].GetContent());
                   SlotUI.InitData(TypeList[j]);
               }
            }
        }

    }
    
}
