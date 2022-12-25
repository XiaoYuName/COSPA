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
        private MoneyUI MoneyUI;
        
        public override void Init()
        {
            MoneyUI = Get<MoneyUI>("UIMask/MoneyUI");
            MoneyUI.Init();
            content = Get<RectTransform>("UIMask/Scroll View/Content");
            InitData();
        }

        
        private void InitData()
        {
            UIHelper.Clear(content);
            characterBags = InventoryManager.Instance.GetAllBag();

            foreach (var data in characterBags)
            {
               CharacterInfo Obj =UISystem.Instance.InstanceUI<CharacterInfo>("CharacterInfo", content);
               Obj.InitData(data);
            }
        }
    }
}

