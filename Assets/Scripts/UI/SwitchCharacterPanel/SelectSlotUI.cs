using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace ARPG.UI
{
    public class SelectSlotUI : UIBase,IPointerDownHandler,IPointerUpHandler
    {
        private Image icon;
        private Button SelectBtn;
        private TextMeshProUGUI level;
        [HideInInspector]public CharacterBag currentdata;
        private bool isDown;
        private float DownTime;

        public override void Init()
        {
            icon = GetComponent<Image>();
            level = Get<TextMeshProUGUI>("Level");
            SelectBtn = GetComponent<Button>();
        }


        public void InitData(int index,CharacterBag data)
        {
            level.text = "lv:"+data.Level;
            currentdata = data;
            CharacterConfigInfo info = InventoryManager.Instance.GetCharacter(data.ID);
            icon.sprite = info.Headicon;
            Bind(SelectBtn, delegate
            {
                UISystem.Instance.GetUI<SwitchCharacterPanel>("SwitchCharacterPanel")
                    .UpdateSlectHead(index,this);
            },"OutChick");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isDown = false;
            DownTime = 0;
        }

        public void Update()
        {
            if (!gameObject.activeSelf || !isDown) return;
            if (DownTime >= Settings.isDownTime)
            {
                isDown = false;
                DownTime = 0;
                void Func(CharacterToolTip ui)
                {
                    ui.ShowCharacterInfo(currentdata);
                    MainPanel.Instance.AddTbaleChild("CharacterToolTip");
                }
                UISystem.Instance.OpenUI<CharacterToolTip>("CharacterToolTip",Func);
            }
            else
            {
                DownTime += Time.deltaTime;
            }
        }
    }
}

