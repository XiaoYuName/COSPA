using System.Collections;
using System.Collections.Generic;
using ARPG.UI.Config;
using UnityEngine;

namespace ARPG.UI
{
    public class OptionContent : UIBase
    {
        private RectTransform content;
        public override void Init()
        {
            content = Get<RectTransform>("UIMask");
        }

        public void InitData(List<DialogOption> options)
        {
            UIHelper.Clear(content);
            for (int i = 0; i < options.Count; i++)
            {
                OptionItemUI optionItemUI =  UISystem.Instance.InstanceUI<OptionItemUI>("OptionItemUI"
                ,content);
                optionItemUI.InitData(options[i]);
            }
        }
        
        public override void Close()
        {
            isOpen = false;
            content.gameObject.SetActive(false);
        }

        public override void Open()
        {
            isOpen = true;
            content.gameObject.SetActive(true);
        }
    }
  
}
