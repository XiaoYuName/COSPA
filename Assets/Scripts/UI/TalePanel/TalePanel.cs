using System.Collections;
using System.Collections.Generic;
using ARPG.Config;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.UI
{
    /// <summary>
    /// 主线剧情面板
    /// </summary>
    public class TalePanel : UIBase
    {
        private RectTransform content;
        private Button CloseBtn;
        public override void Init()
        {
            content = Get<RectTransform>("UIMask/TaleView/Viewport/Content");
            CloseBtn = Get<Button>("UIMask/Close");
            Bind(CloseBtn,Close,"OnChick");
            CreatTaleItemUI();
        }

        private void CreatTaleItemUI()
        {
            UIHelper.Clear(content);
            List<TaleItemData> itemDatas = ConfigSystem.Instance.GetAllTale();
            foreach (var data in itemDatas)
            {
                TaleItemUI ItemUI =  UISystem.Instance.InstanceUI<TaleItemUI>("TaleItemUI",content);
                ItemUI.InitData(data);
            }
        }

        public override void Close()
        {
            base.Close();
            MainPanel.Instance.RemoveTableChild("TalePanel");
        }
    }
}

