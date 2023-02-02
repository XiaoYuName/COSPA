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
            UISystem.Instance.ShowPopDialogue("提示","要进入"+Title+"剧情吗？","进入","关闭",
                delegate
                {
                    FadeManager.Instance.PlayFade(0.45f, delegate
                    {
                        DialogueManager manager = DialogueManager.Instance.GetComponent<DialogueManager>();
                        manager.StarPlayDialogueUI(curretnData.DialogueID);
                    },0.15f);
                }, null);
            
        }
    }
}

