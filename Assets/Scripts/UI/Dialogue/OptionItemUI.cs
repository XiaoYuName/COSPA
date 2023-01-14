using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class OptionItemUI : UIBase
    {
        private Button GoToTargetBtn;
        private TextMeshProUGUI Text;
        private Image FarmeIcon;
        private DialogOption data;
        
        public override void Init()
        {
            FarmeIcon = GetComponent<Image>();
            GoToTargetBtn = GetComponent<Button>();
            Text = Get<TextMeshProUGUI>("Value");
            Bind(GoToTargetBtn,OnChick,"OnChick");
        }
        
        public void InitData(DialogOption option)
        {
            data = option;
            Text.text = option.OpentionText;
            FarmeIcon.sprite = GameSystem.Instance.GetSprite(option.Mode.ToString());
        }


        public void OnChick()
        {
            if(data == null)return;
            DialogueManager manager = DialogueManager.Instance.GetComponent<DialogueManager>();
            manager.ToTargetDialogue(data.targetPirecID);
        }
    }
}

