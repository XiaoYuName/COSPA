using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using ARPG.UI.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class CharacterSlotUI : UIBase
    {
        private Image icon;
        private int index;
        private GameObject SelectIcon;
        private Button SelectBtn;
        private TextMeshProUGUI level;
        [HideInInspector]public CharacterBag currentdata;
        public override void Init()
        {
            icon = GetComponent<Image>();
            index = default;
            SelectIcon = Get("Select");
            level = Get<TextMeshProUGUI>("Level");
            SelectBtn = GetComponent<Button>();
            SetSelect(false);
        }


        public void InitData(int index,CharacterBag data)
        {
            currentdata = data;
            this.index = index;
            level.text = "lv:"+data.Level;
            CharacterConfigInfo info = InventoryManager.Instance.GetCharacter(data.ID);
            icon.sprite = info.Headicon;
            Bind(SelectBtn,()=>
                UISystem.Instance.GetUI<SwitchCharacterPanel>("SwitchCharacterPanel").SetSelect(this.index),
                UI_ToolAudio.UI_click.ToString());
        }

        public void SetSelect(bool active)
        {
            SelectIcon.gameObject.SetActive(active);
            SelectBtn.interactable = !active;
        }


        public void InitData(EnemyBag data)
        {
            
        }
    }
}

