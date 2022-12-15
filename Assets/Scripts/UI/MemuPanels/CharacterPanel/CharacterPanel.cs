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
            content = Get<RectTransform>("UIMask/Scroll View/Content");
            InitData();
        }

        
        private void InitData()
        {
            UIHelper.Clear(content);
            characterBags = InventoryManager.Instance.GetBag();

            foreach (var data in characterBags)
            {
               CharacterInfo Obj =UISystem.Instance.InstanceUI<CharacterInfo>("CharacterInfo", content);
               Obj.InitData(data);
            }
        }
    }
}

