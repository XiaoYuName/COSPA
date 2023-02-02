using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class TaleItemUI : UIBase
    {
        private TextMeshProUGUI Names;
        private TextMeshProUGUI Title;
        private TextMeshProUGUI description;
        private Button OnOpenDialogueBtn;
        private TaleItemData curretnData;
        public override void Init()
        {
            Names = Get<TextMeshProUGUI>("Names");
            Title = Get<TextMeshProUGUI>("Title");
            description = Get<TextMeshProUGUI>("description");
            OnOpenDialogueBtn = GetComponent<Button>();
            Bind(OnOpenDialogueBtn,OnClick,"OnChick");
        }

        public void InitData(TaleItemData data)
        {
            curretnData = data;
            Names.text = data.titleName;
            Title.text = data.ItemName;
            description.text = data.description;
        }


        private void OnClick()
        {
            if (curretnData == null) return;
        }
    }
}

