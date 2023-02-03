using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class SwitchBuffUI : UIBase
    {
        private Image FarmeIcon;
        private Image icon;
        private TextMeshProUGUI Name;
        private TextMeshProUGUI description;
        private Image Highlight;
        private Button OnClickBtn;
        private BuffData currentdata;
        private bool Active;
        public override void Init()
        {
            FarmeIcon = GetComponent<Image>();
            icon = Get<Image>("icon");
            Name = Get<TextMeshProUGUI>("Name");
            description = Get<TextMeshProUGUI>("Description");
            Highlight = Get<Image>("Highlight");
            OnClickBtn = GetComponent<Button>();
            Bind(OnClickBtn,OnClick,"OnChick");
            Active = false;
        }

        public void IniData(string ID)
        {
            BuffData buffData = ConfigSystem.Instance.GetBUFFData(ID);
            currentdata = buffData ?? throw new Exception("未匹配的BUFF");
            icon.sprite = GameSystem.Instance.GetSprite(buffData.SpriteID);
            Name.text = buffData.BuffName;
            description.text = buffData.description;
            FarmeIcon.sprite = GameSystem.Instance.GetSprite(buffData.behaviourType.ToString());
        }

        private void OnClick()
        {
            if (currentdata == null) return;
            SwitchBuffPanel UIPanel = UISystem.Instance.GetUI<SwitchBuffPanel>("SwitchBuffPanel");

            if (!Active)
            {
                if (UIPanel.Add_BUFF(currentdata))
                {
                    Highlight.gameObject.SetActive(true);
                    Active = true;
                }
            }
            else
            {
                if (UIPanel.Remove_BUFF(currentdata))
                {
                    Highlight.gameObject.SetActive(false);
                    Active = false;
                }
            }


        }
    }
}

