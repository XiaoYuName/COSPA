using System.Collections;
using System.Collections.Generic;
using ARPG;
using ARPG.Config;
using ARPG.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SkillSlotUI : UIBase
    {
        public SkillType type;
        private TextMeshProUGUI SkillName;
        private TextMeshProUGUI SkillMode;
        private TextMeshProUGUI SkillDes;
        private TextMeshProUGUI SkillCD;
        private TextMeshProUGUI SkillRadius;
        private GameObject NotPanel;
        private TextMeshProUGUI StepUpDes;
        private Image icon;
        
        public override void Init()
        {
            SkillName = Get<TextMeshProUGUI>("Mask/SkillName");
            SkillMode = Get<TextMeshProUGUI>("Mask/SkilMode");
            SkillDes = Get<TextMeshProUGUI>("Mask/SkillDes");
            SkillCD = Get<TextMeshProUGUI>("Mask/SkilCD");
            SkillRadius = Get<TextMeshProUGUI>("Mask/SkilRadius");
            NotPanel = Get("NotPanel");
            icon = Get<Image>("icon");
            StepUpDes = Get<TextMeshProUGUI>("NotPanel/SkillDes");
        }

        public void InitData(int currentStar,SkillType SkillType,SkillItem data)
        {
            NotPanel.gameObject.SetActive(currentStar < data.ActionStar);
            type = SkillType;
            SkillName.text = data.SkillName;
            SkillMode.text = data.SkillType.type.ToString();
            SkillCD.text = "CD:"+data.CD;
            SkillRadius.text = "范围:" + data.Radius+"xp";
            icon.sprite = data.icon;
            SkillDes.text = data.SkillDes;
            StepUpDes.text = data.StepUpDes;

        }

        
    }
}

