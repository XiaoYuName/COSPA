using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    public class CharacterPanel : UIBase
    {
        private RectTransform content;
        private List<CharacterBag> characterBags;
        public override void Init()
        {
            content = Get<RectTransform>("UIMask/Character/Mask/Scroll Rect/Content");
            InitData();
            MessageAction.RefreshCharacterBag += RefInitData;
        }

        
        private void InitData()
        {
            UIHelper.Clear(content);
            characterBags = InventoryManager.Instance.GetCharacterAllBag();

            foreach (var data in characterBags)
            {
               CharacterInfo Obj =UISystem.Instance.InstanceUI<CharacterInfo>("CharacterInfo", content);
               Obj.InitData(data);
            }
        }

        private void RefInitData(List<CharacterBag> character)
        {
            UIHelper.Clear(content);
            foreach (var data in character)
            {
                CharacterInfo Obj =UISystem.Instance.InstanceUI<CharacterInfo>("CharacterInfo", content);
                Obj.InitData(data);
            }
        }

        private void OnDestroy()
        {
            MessageAction.RefreshCharacterBag -= RefInitData;
        }
    }
}

