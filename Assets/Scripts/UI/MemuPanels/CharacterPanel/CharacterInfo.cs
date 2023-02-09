using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class CharacterInfo : UIBase
    {
        private Image icon;
        private StarContent StarContent;
        private TextMeshProUGUI Level;
        private Button btn;
        [HideInInspector]public string CharacterID;
        public override void Init()
        {
            icon = Get<Image>("Back/Icon");
            StarContent = Get<StarContent>("StarContent");
            Level = Get<TextMeshProUGUI>("Back/Level");
            btn = Get<Button>("Back");
            MessageAction.UpCharacterBag += RefData;
        }

        public void InitData(CharacterBag info)
        {
            CharacterConfigInfo character = InventoryManager.Instance.GetCharacter(info.ID);
            CharacterID = info.ID;
            icon.sprite = character.GetAssets(info.currentStar).CharacterPanelIcon;
            Level.text = "等级 "+info.Level;
            SetInfoStar(info.currentStar);
            Bind(btn, delegate
            {
                FadeManager.Instance.PlayFade(0.25f, delegate
                {
                    void func(CharacterEquipPanel ui)
                    {
                        ui.InitData(info);
                    }
                    MainPanel.Instance.AddTbaleChild("CharacterEquipPanel");
                    UISystem.Instance.OpenUI<CharacterEquipPanel>("CharacterEquipPanel",func);
                },1);
            }, "UI_click");
        }

        /// <summary>
        /// 设置显示的星级
        /// </summary>
        /// <param name="star"></param>
        private void SetInfoStar(int star)
        {
            StarContent.Show(star);
        }

        private void RefData(CharacterBag Bag)
        {
            if (CharacterID == Bag.ID)
            {
                InitData(Bag);
            }
        }

        public void OnDestroy()
        {
            MessageAction.UpCharacterBag -= RefData;
        }
    }
}

